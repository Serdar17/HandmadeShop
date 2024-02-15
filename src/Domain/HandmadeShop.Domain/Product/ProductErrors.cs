using HandmadeShop.Domain.Common;

namespace HandmadeShop.Domain;

public static class ProductErrors
{
    public static Error NotFound(Guid productId) =>
        Error.NotFound("Products.NotFound", $"Product with id={productId} was not found");
}