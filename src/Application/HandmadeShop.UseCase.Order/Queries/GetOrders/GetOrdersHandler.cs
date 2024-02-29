using AutoMapper;
using HandmadeShop.Application.Abstraction.Messaging;
using HandmadeShop.Domain;
using HandmadeShop.Domain.Common;
using HandmadeShop.Infrastructure.Abstractions.Context;
using HandmadeShop.Infrastructure.Abstractions.Identity;
using HandmadeShop.SharedModel.Orders.Models;
using Microsoft.AspNetCore.Identity;

namespace HandmadeShop.UseCase.Order.Queries.GetOrders;

internal sealed class GetOrdersHandler : IQueryHandler<GetOrdersQuery, IEnumerable<OrderModel>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IIdentityService _identityService;
    private readonly UserManager<User> _userManager;

    public GetOrdersHandler(
        IUnitOfWork unitOfWork, 
        IIdentityService identityService,
        IMapper mapper,
        UserManager<User> userManager)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _identityService = identityService;
        _userManager = userManager;
    }

    public async Task<Result<IEnumerable<OrderModel>>> Handle(GetOrdersQuery request, CancellationToken cancellationToken)
    {
        var userId = _identityService.GetUserIdentity();
        var user = await _userManager.FindByIdAsync(userId.ToString());

        if (user is null)
        {
            return UserErrors.NotFound(userId);
        }

        var orders = await _unitOfWork.OrderRepository
            .GetAllAsync(p => p.Buyer.UserId == userId);
        
        var model =_mapper.Map<IEnumerable<OrderModel>>(orders.ToList());
        return Result<IEnumerable<OrderModel>>.Success(model);
    }
}