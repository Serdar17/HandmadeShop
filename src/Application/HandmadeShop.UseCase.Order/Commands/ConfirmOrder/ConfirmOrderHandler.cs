using AutoMapper;
using HandmadeShop.Application.Abstraction.Messaging;
using HandmadeShop.Domain;
using HandmadeShop.Domain.Common;
using HandmadeShop.Infrastructure.Abstractions.Context;
using HandmadeShop.Infrastructure.Abstractions.Identity;
using HandmadeShop.SharedModel.Orders.Models;
using Microsoft.AspNetCore.Identity;

namespace HandmadeShop.UseCase.Order.Commands.ConfirmOrder;

internal sealed class ConfirmOrderHandler(
    IUnitOfWork unitOfWork,
    IIdentityService identityService,
    UserManager<User> userManager,
    IMapper mapper)
    : ICommandHandler<ConfirmOrderCommand, OrderModel>
{
    public async Task<Result<OrderModel>> Handle(ConfirmOrderCommand request, CancellationToken cancellationToken)
    {
        var userId = identityService.GetUserIdentity();
        var user = await userManager.FindByIdAsync(userId.ToString());

        if (user is null)
        {
            return UserErrors.NotFound(userId);
        }

        var order = await unitOfWork.OrderRepository.GetByIdAsync(request.Model.OrderId, cancellationToken);

        if (order is null)
        {
            return OrderErrors.NotFound(request.Model.OrderId);
        }
        
        order.SetAcceptedStatus();

        await unitOfWork.OrderRepository.UpdateAsync(order, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        
        return mapper.Map<OrderModel>(order);
    }
}