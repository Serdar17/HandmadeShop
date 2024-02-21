namespace HandmadeShop.SharedModel.Reviews.Models;

public class ReviewModel
{
    public Guid ProductId { get; set; }
    
    public string Comment { get; set; }
    public int Rating { get; set; }
    
}