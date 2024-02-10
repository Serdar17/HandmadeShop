using HandmadeShop.Domain.Common;

namespace HandmadeShop.Domain;

public class UserError
{
    public static Error SameUser = Error.Conflict("User.SameUser", "User should have unique email");
}