namespace HandmadeShop.Api.Controllers.Order.Models;

/// <summary>
/// Address request
/// </summary>
public class AddressRequest
{
    /// <summary>
    /// Country
    /// </summary>
    public required string Country { get; set; }
    
    /// <summary>
    /// City
    /// </summary>
    public required string City { get; set; }
    
    /// <summary>
    /// Exact address
    /// </summary>
    public required string ExactAddress { get; set; }
}