namespace HandmadeShop.Domain;

public enum OrderStatus
{
    Pending = 0,
    StockConfirmed,
    Sent,
    Shipped,
    Cancelled,
    Accepted
}