using HandmadeShop.Domain.Common;

namespace HandmadeShop.Domain;

public class Like : BaseEntity
{
    private int _quantity;

    public int Quantity
    {
        get => _quantity;
        set
        {
            if (value < 1)
                throw new ArgumentException(nameof(value));

            _quantity = value;
        }
    }

    public int ProductId { get; set; }
    public virtual Product Product { get; set; }

    public virtual ICollection<User> Users { get; set; } = new List<User>();
    
}