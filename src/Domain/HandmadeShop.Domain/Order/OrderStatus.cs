namespace HandmadeShop.Domain;

public enum OrderStatus
{
    Pending,
    WaitingForSent,
    StockConfirmed,
    Shipping,
    Cancelled,
    Taken
}