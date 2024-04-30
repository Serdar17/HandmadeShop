using AutoMapper;
using HandmadeShop.Application.Abstraction.Messaging;
using HandmadeShop.Domain;
using HandmadeShop.Domain.Common;
using HandmadeShop.Infrastructure.Abstractions.Context;
using HandmadeShop.Infrastructure.Abstractions.FileStorage;
using HandmadeShop.Infrastructure.Abstractions.Identity;
using HandmadeShop.SharedModel.Catalogs.Models;
using HandmadeShop.UserCase.Catalog.Models;
using Microsoft.AspNetCore.Identity;

namespace HandmadeShop.UserCase.Catalog.Commands.CreateProduct;

internal sealed class CreateProductCommandHandler(
    IUnitOfWork unitOfWork,
    IMapper mapper,
    IIdentityService identityService,
    UserManager<User> userManager,
    IFileStorage fileStorage)
    : ICommandHandler<CreateProductCommand, ProductModel>
{
    private readonly IFileStorage _fileStorage = fileStorage;

    public async Task<Result<ProductModel>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var userId = identityService.GetUserIdentity();
        var user = await userManager.FindByIdAsync(userId.ToString());

        if (user is null)
        {
            return UserErrors.NotFound(userId);
        }

        var product = mapper.Map<Product>(request.Model);
        var catalog = await unitOfWork.CatalogRepository.GetByNameAsync(request.Model.CatalogName) 
                      ?? new Domain.Catalog(request.Model.CatalogName);
        
        product.Catalog = catalog;
        user.Products.Add(product);

        var result = await userManager.UpdateAsync(user);

        if (!result.Succeeded)
        {
            return UserErrors.UpdateError(string.Join(", ", result.Errors.Select(x => x.Description)));
        }
        
        return mapper.Map<ProductModel>(product);
    }
}