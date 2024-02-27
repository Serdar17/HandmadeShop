using HandmadeShop.Domain.Common;

namespace HandmadeShop.Domain;

public static class CartErrors
{
    public static Error NotFound(Guid userId) =>
        Error.NotFound("Carts.NotFound", $"Cart for user with id={userId} was not found");
}