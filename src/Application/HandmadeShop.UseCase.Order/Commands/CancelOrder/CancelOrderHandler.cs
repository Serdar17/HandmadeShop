using AutoMapper;
using HandmadeShop.Application.Abstraction.Messaging;
using HandmadeShop.Domain;
using HandmadeShop.Domain.Common;
using HandmadeShop.Infrastructure.Abstractions.Context;
using HandmadeShop.Infrastructure.Abstractions.Identity;
using HandmadeShop.SharedModel.Orders.Models;
using Microsoft.AspNetCore.Identity;

namespace HandmadeShop.UseCase.Order.Commands.CancelOrder;

internal sealed class CancelOrderHandler : ICommandHandler<CancelOrderCommand, OrderModel>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IIdentityService _identityService;
    private readonly UserManager<User> _userManager;
    private readonly IMapper _mapper;

    public CancelOrderHandler(
        IUnitOfWork unitOfWork,
        IIdentityService identityService,
        UserManager<User> userManager, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _identityService = identityService;
        _userManager = userManager;
        _mapper = mapper;
    }

    public async Task<Result<OrderModel>> Handle(CancelOrderCommand request, CancellationToken cancellationToken)
    {
        var userId = _identityService.GetUserIdentity();
        var user = await _userManager.FindByIdAsync(userId.ToString());

        if (user is null)
        {
            return UserErrors.NotFound(userId);
        }

        var order = await _unitOfWork.OrderRepository.GetByIdAsync(request.Model.OrderId, cancellationToken);

        if (order is null)
        {
            return OrderErrors.NotFound(request.Model.OrderId);
        }
        
        order.SetCancelledStatus();
        await _unitOfWork.OrderRepository.UpdateAsync(order, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return _mapper.Map<OrderModel>(order);
    }
}