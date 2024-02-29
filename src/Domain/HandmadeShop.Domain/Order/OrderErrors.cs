using HandmadeShop.Domain.Common;

namespace HandmadeShop.Domain;

public static class OrderErrors
{
    public static Error NotFound(Guid orderId) =>
        Error.NotFound("Orders.NotFound", $"Order with id={orderId} was not found");
    
    public static Error Prohibition() =>
        Error.Forbidden("Orders.OrderDetails", "Access to reading the order details is prohibited");
}