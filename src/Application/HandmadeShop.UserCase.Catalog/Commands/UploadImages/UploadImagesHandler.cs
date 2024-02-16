using AutoMapper;
using HandmadeShop.Application.Abstraction.Messaging;
using HandmadeShop.Common.Constants;
using HandmadeShop.Domain;
using HandmadeShop.Domain.Common;
using HandmadeShop.Infrastructure.Abstractions.Context;
using HandmadeShop.Infrastructure.Abstractions.FileStorage;
using HandmadeShop.UserCase.Catalog.Models;

namespace HandmadeShop.UserCase.Catalog.Commands.UploadImages;

internal sealed class UploadImagesHandler : ICommandHandler<UploadImagesCommand, UploadedImageModel>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IFileStorage _fileStorage;
    private readonly IMapper _mapper;

    public UploadImagesHandler(
        IUnitOfWork unitOfWork, 
        IMapper mapper, 
        IFileStorage fileStorage)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _fileStorage = fileStorage;
    }

    public async Task<Result<UploadedImageModel>> Handle(UploadImagesCommand request, CancellationToken cancellationToken)
    {
        var product = await _unitOfWork.ProductRepository.GetByIdAsync(request.ProductId, cancellationToken);

        if (product is null)
        {
            return ProductErrors.NotFound(request.ProductId);
        }

        // await UploadImagesAsync(product, request.Model);

        var path = await _fileStorage.UploadAsync(
            product.Uid,
            request.Model.Image,
            FolderPaths.PathToProductImagesFolder);
            
        product.Images.Add(path);
        
        await _unitOfWork.ProductRepository.UpdateAsync(product, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        var model = new UploadedImageModel()
        {
            ImagePath = path,
            DownloadUrl = await _fileStorage.GetDownloadLinkAsync(path, cancellationToken)
        };
        
        return model;
    }

    private async Task UploadImagesAsync(Product product, UploadImagesModel model)
    {
        
        // foreach (var file in model.Images)
        // {
        //     var path = await _fileStorage.UploadAsync(
        //         product.Uid,
        //         file,
        //         FolderPaths.PathToProductImagesFolder);
        //     
        //     product.Images.Add(path);
        // }
    }
}