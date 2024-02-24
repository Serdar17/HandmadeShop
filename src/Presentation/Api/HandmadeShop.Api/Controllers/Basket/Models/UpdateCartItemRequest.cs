namespace HandmadeShop.Api.Controllers.Basket.Models;

/// <summary>
/// Update cart item model
/// </summary>
public class UpdateCartItemRequest
{
    /// <summary>
    /// Product id
    /// </summary>
    public Guid ProductId { get; set; }
    
    /// <summary>
    /// Quantity
    /// </summary>
    public double Quantity { get; set; }
}