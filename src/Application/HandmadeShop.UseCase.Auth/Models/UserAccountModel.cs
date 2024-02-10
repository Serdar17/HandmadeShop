using AutoMapper;
using HandmadeShop.Domain;

namespace HandmadeShop.UseCase.Auth.Models;

public class UserAccountModel
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public required string Email { get; set; }
}

public class UserAccountModelProfile : Profile
{
    public UserAccountModelProfile()
    {
        CreateMap<User, UserAccountModel>();
    }
}