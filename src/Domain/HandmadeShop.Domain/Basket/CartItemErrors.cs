using HandmadeShop.Domain.Common;

namespace HandmadeShop.Domain.Basket;

public class CartItemErrors
{
    public static Error NotFound(Guid productId) =>
        Error.NotFound("CartItem.NotFound", $"Cart item for product with id={productId} was not found");
}