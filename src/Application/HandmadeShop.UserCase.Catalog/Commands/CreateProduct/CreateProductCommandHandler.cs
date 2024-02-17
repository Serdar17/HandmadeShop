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

internal sealed class CreateProductCommandHandler : ICommandHandler<CreateProductCommand, ProductModel>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IIdentityService _identityService;
    private readonly UserManager<User> _userManager;
    private readonly IFileStorage _fileStorage;

    public CreateProductCommandHandler(
        IUnitOfWork unitOfWork, 
        IMapper mapper, 
        IIdentityService identityService,
        UserManager<User> userManager, IFileStorage fileStorage)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _identityService = identityService;
        _userManager = userManager;
        _fileStorage = fileStorage;
    }

    public async Task<Result<ProductModel>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var userId = _identityService.GetUserIdentity();
        var user = await _userManager.FindByIdAsync(userId.ToString());

        if (user is null)
        {
            return UserErrors.NotFound(userId);
        }

        var product = _mapper.Map<Product>(request.Model);
        var catalog = await _unitOfWork.CatalogRepository.GetByNameAsync(request.Model.CatalogName) 
                      ?? new Domain.Catalog(request.Model.CatalogName);
        
        product.Catalog = catalog;
        user.Products.Add(product);

        var result = await _userManager.UpdateAsync(user);

        if (!result.Succeeded)
        {
            return UserErrors.UpdateError(string.Join(", ", result.Errors.Select(x => x.Description)));
        }
        
        return _mapper.Map<ProductModel>(product);
    }
}