using AutoMapper;
using HandmadeShop.Domain;
using HandmadeShop.Domain.Enums;

namespace HandmadeShop.UseCase.Account.Models;

public class UpdateAccountInfoModel
{
    public required string Name { get; set; }
    public Gender Gender { get; set; }
    public DateTime BirthDate { get; set; }
}

public class UpdateAccountInfoModelProfile : Profile
{
    public UpdateAccountInfoModelProfile()
    {
        CreateMap<UpdateAccountInfoModel, User>();
    }
}