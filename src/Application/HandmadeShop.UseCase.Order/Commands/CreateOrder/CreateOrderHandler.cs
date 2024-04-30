using AutoMapper;
using HandmadeShop.Application.Abstraction.Messaging;
using HandmadeShop.Domain;
using HandmadeShop.Domain.Basket;
using HandmadeShop.Domain.Common;
using HandmadeShop.Infrastructure.Abstractions.Caching;
using HandmadeShop.Infrastructure.Abstractions.Context;
using HandmadeShop.Infrastructure.Abstractions.Identity;
using Microsoft.AspNetCore.Identity;

namespace HandmadeShop.UseCase.Order.Commands.CreateOrder;

internal sealed class CreateOrderHandler(
    IUnitOfWork unitOfWork,
    IIdentityService identityService,
    UserManager<User> userManager,
    ICacheService cache,
    IMapper mapper)
    : ICommandHandler<CreateOrderCommand>
{
    public async Task<Result> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        var userId = identityService.GetUserIdentity();
        var user = await userManager.FindByIdAsync(userId.ToString());
        
        if (user is null)
        {
            return UserErrors.NotFound(userId);
        }

        var cart = await cache.GetAsync<Cart>(userId.ToString(), cancellationToken: cancellationToken);

        if (cart is null)
        {
            return CartErrors.NotFound(userId);
        }

        var order = mapper.Map<Domain.Order>(request.Model.Order);
        var buyer = mapper.Map<Buyer>(request.Model.Buyer);
        buyer.UserId = userId;
        var orderItems = mapper.Map<IEnumerable<OrderItem>>(cart.Items);
        order.Buyer = buyer;
        order.Items = orderItems.ToList();

        await unitOfWork.OrderRepository.InsertAsync(order, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        
        return Result.Success();
    }
}