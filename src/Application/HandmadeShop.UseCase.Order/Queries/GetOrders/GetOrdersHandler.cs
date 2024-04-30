using AutoMapper;
using HandmadeShop.Application.Abstraction.Messaging;
using HandmadeShop.Domain;
using HandmadeShop.Domain.Common;
using HandmadeShop.Infrastructure.Abstractions.Context;
using HandmadeShop.Infrastructure.Abstractions.Identity;
using HandmadeShop.SharedModel.Orders.Models;
using Microsoft.AspNetCore.Identity;

namespace HandmadeShop.UseCase.Order.Queries.GetOrders;

internal sealed class GetOrdersHandler(
    IUnitOfWork unitOfWork,
    IIdentityService identityService,
    IMapper mapper,
    UserManager<User> userManager)
    : IQueryHandler<GetOrdersQuery, IEnumerable<OrderModel>>
{
    public async Task<Result<IEnumerable<OrderModel>>> Handle(GetOrdersQuery request, CancellationToken cancellationToken)
    {
        var userId = identityService.GetUserIdentity();
        var user = await userManager.FindByIdAsync(userId.ToString());

        if (user is null)
        {
            return UserErrors.NotFound(userId);
        }
        
        var orderQuery = await unitOfWork.OrderRepository
            .GetAllAsync(p => p.Buyer.UserId == userId);

        if (request.Model.Status != null)
        {
            if (request.Model.Status == OrderStatus.Pending)
            {
                orderQuery = orderQuery.Where(x => x.OrderStatus == OrderStatus.Pending ||
                                                   x.OrderStatus == OrderStatus.Sent ||
                                                   x.OrderStatus == OrderStatus.Shipped || 
                                                   x.OrderStatus == OrderStatus.StockConfirmed);
            }
            else
            {
                orderQuery = orderQuery.Where(x => x.OrderStatus == request.Model.Status);
            }
        }
        
        var model =mapper.Map<IEnumerable<OrderModel>>(orderQuery.ToList());
        return Result<IEnumerable<OrderModel>>.Success(model);
    }
}