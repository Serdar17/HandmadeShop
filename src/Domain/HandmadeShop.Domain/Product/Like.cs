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
            if (value < 0)
                throw new ArgumentException(nameof(value));
            
            _quantity = value;

            if (_quantity < 0)
                _quantity = 0;
            
        }
    }

    public int ProductId { get; set; }
    public virtual Product Product { get; set; }

    // public virtual ICollection<User> Users { get; set; } = new List<User>();

    public virtual ICollection<UserLike> UserLikes { get; set; } = new List<UserLike>();
}