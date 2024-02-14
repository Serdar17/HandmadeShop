using AutoMapper;
using HandmadeShop.Domain;
using HandmadeShop.Domain.Enums;
using HandmadeShop.Infrastructure.Abstractions.FileStorage;

namespace HandmadeShop.UseCase.Account.Models;

public class AccountInfoModel
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public required string Email { get; set; }
    public Gender Gender { get; set; }
    public string? DownloadPath { get; set; }
    public string? AvatarUrl { get; set; }
    public DateTime? BirthDate { get; set; }
}

public class AccountInfoModelProfile : Profile
{
    public AccountInfoModelProfile()
    {
        CreateMap<User, AccountInfoModel>()
            .AfterMap<AccountInfoModelActions>();
    }
    
    public class AccountInfoModelActions : IMappingAction<User, AccountInfoModel>
    {
        private readonly IFileStorage _fileStorage;

        public AccountInfoModelActions(IFileStorage fileStorage)
        {
            _fileStorage = fileStorage;
        }

        public void Process(User source, AccountInfoModel destination, ResolutionContext context)
        {
            if (source.AvatarUrl is null)
                return;
            
            var url =  _fileStorage.GetDownloadLinkAsync(source.AvatarUrl)
                .GetAwaiter()
                .GetResult();
            
            destination.DownloadPath = url;
        }
    }
}