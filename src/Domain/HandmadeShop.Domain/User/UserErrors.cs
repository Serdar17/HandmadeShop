using HandmadeShop.Domain.Common;

namespace HandmadeShop.Domain;

public class UserErrors
{
    public static Error SameUser = Error.Conflict("User.SameUser", "User should have unique email");

    public static Error NotFound(Guid userId) =>
        Error.NotFound("Users.NotFound", $"User with id={userId} was not found");

    public static Error CreateError(string message) =>
        Error.Conflict("Users.CreateUser", message);

    public static Error VerifyEmail(string message) =>
        Error.Conflict("Users.VerifyEmail", message);
}