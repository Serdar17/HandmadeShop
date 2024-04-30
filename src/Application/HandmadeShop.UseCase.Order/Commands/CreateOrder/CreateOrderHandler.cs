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

internal sealed class CreateOrderHandler : ICommandHandler<CreateOrderCommand>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IIdentityService _identityService;
    private readonly UserManager<User> _userManager;
    private readonly ICacheService _cache;
    private readonly IMapper _mapper;

    public CreateOrderHandler(
        IUnitOfWork unitOfWork, 
        IIdentityService identityService, 
        UserManager<User> userManager, 
        ICacheService cache, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _identityService = identityService;
        _userManager = userManager;
        _cache = cache;
        _mapper = mapper;
    }

    public async Task<Result> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        var userId = _identityService.GetUserIdentity();
        var user = await _userManager.FindByIdAsync(userId.ToString());
        
        if (user is null)
        {
            return UserErrors.NotFound(userId);
        }

        var cart = await _cache.GetAsync<Cart>(userId.ToString(), cancellationToken: cancellationToken);

        if (cart is null)
        {
            return CartErrors.NotFound(userId);
        }

        var order = _mapper.Map<Domain.Order>(request.Model.Order);
        var buyer = _mapper.Map<Buyer>(request.Model.Buyer);
        buyer.UserId = userId;
        var orderItems = _mapper.Map<IEnumerable<OrderItem>>(cart.Items);
        order.Buyer = buyer;
        order.Items = orderItems.ToList();

        await _unitOfWork.OrderRepository.InsertAsync(order, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        
        return Result.Success();
    }
}