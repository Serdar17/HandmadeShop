using HandmadeShop.Domain.Common;

namespace HandmadeShop.Domain;

public class UserLike : BaseEntity
{
    public Guid UserId { get; set; }
    public virtual User User { get; set; }
    
    public int LikeId { get; set; }
    public virtual Like Like { get; set; }

    // public UserLike(Guid userId, int likeId)
    // {
    //     UserId = userId;
    //     LikeId = likeId;
    // }
    
}