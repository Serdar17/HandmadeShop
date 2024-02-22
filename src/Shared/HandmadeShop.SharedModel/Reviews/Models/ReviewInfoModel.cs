namespace HandmadeShop.SharedModel.Reviews.Models;

public class ReviewInfoModel
{
    public Guid Uid { get; set; }
    public string Comment { get; set; }
    public int Rating { get; set; }
    public DateTime CreatedAt { get; set; }
    public OwnerModel Owner { get; set; }

    public List<string> ImageUrls { get; set; } = new();
}

public class OwnerModel
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
}