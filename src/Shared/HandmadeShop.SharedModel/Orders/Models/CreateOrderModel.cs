namespace HandmadeShop.SharedModel.Orders.Models;

public class CreateOrderModel
{
    public BuyerModel Buyer { get; set; }
    public OrderModel Order { get; set; }

    public CreateOrderModel()
    {
        Buyer = new BuyerModel();
        Order = new OrderModel();
        Order.Address = new AddressModel();
    }
}