namespace HandmadeShop.SharedModel.Orders.Models;

public class OrderItemModel
{
    public Guid ProductId { get; set; }
    public string ProductName { get; set; }
    public double Price { get; set; }
    public int Quantity { get; set; }
    public string? ImagePath { get; set; }
    public string? DownloadUrl { get; set; }
}