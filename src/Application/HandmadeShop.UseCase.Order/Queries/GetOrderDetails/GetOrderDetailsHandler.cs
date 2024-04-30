using AutoMapper;
using HandmadeShop.Application.Abstraction.Messaging;
using HandmadeShop.Domain;
using HandmadeShop.Domain.Common;
using HandmadeShop.Infrastructure.Abstractions.Context;
using HandmadeShop.Infrastructure.Abstractions.Identity;
using HandmadeShop.SharedModel.Orders.Models;
using Microsoft.AspNetCore.Identity;

namespace HandmadeShop.UseCase.Order.Queries.GetOrderDetails;

internal sealed class GetOrderDetailsHandler(
    IUnitOfWork unitOfWork,
    IMapper mapper,
    IIdentityService identityService,
    UserManager<User> userManager)
    : IQueryHandler<GetOrderDetailsQuery, OrderModel>
{
    public async Task<Result<OrderModel>> Handle(GetOrderDetailsQuery request, CancellationToken cancellationToken)
    {
        var userId = identityService.GetUserIdentity();
        var user = await userManager.FindByIdAsync(userId.ToString());
        
        if (user is null)
        {
            return UserErrors.NotFound(userId);
        }

        var order = await unitOfWork.OrderRepository.GetByIdAsync(request.OrderId, cancellationToken);

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

        return mapper.Map<OrderModel>(order);
    }
}