using AutoMapper;
using HandmadeShop.Application.Abstraction.Messaging;
using HandmadeShop.Domain;
using HandmadeShop.Domain.Common;
using HandmadeShop.Infrastructure.Abstractions.Context;
using HandmadeShop.Infrastructure.Abstractions.Identity;
using HandmadeShop.SharedModel.Orders.Models;
using Microsoft.AspNetCore.Identity;

namespace HandmadeShop.UseCase.Order.Queries.GetOrderDetails;

internal sealed class GetOrderDetailsHandler : IQueryHandler<GetOrderDetailsQuery, OrderModel>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IIdentityService _identityService;
    private readonly UserManager<User> _userManager;

    public GetOrderDetailsHandler(
        IUnitOfWork unitOfWork, 
        IMapper mapper, 
        IIdentityService identityService, 
        UserManager<User> userManager)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _identityService = identityService;
        _userManager = userManager;
    }

    public async Task<Result<OrderModel>> Handle(GetOrderDetailsQuery request, CancellationToken cancellationToken)
    {
        var userId = _identityService.GetUserIdentity();
        var user = await _userManager.FindByIdAsync(userId.ToString());
        
        if (user is null)
        {
            return UserErrors.NotFound(userId);
        }

        var order = await _unitOfWork.OrderRepository.GetByIdAsync(request.OrderId, cancellationToken);

        if (order is null)
        {
            return OrderErrors.NotFound(request.OrderId);
        }
        
        // if the id of the user who makes a request to receive details of a product
        // that is not his own, then deny access 
        if (order.Buyer.UserId != userId)
        {
            return OrderErrors.Prohibition();
        }

        return _mapper.Map<OrderModel>(order);
    }
}