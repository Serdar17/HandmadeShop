using AutoMapper;

namespace HandmadeShop.UseCase.Auth.Models;

public class RegisterUserModel
{
    public required string Name { get; set; }
    public required string Email { get; set; }
    public required string Password { get; set; }
}

public class RegisterUserModelProfile : Profile
{
    public RegisterUserModelProfile()
    {
        CreateMap<RegisterUserModel, UserAccountModel>();
    }
}
