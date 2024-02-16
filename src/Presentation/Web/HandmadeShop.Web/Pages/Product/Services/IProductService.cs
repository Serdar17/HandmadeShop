using HandmadeShop.Domain.Common;
using HandmadeShop.Web.Pages.Product.Models;

namespace HandmadeShop.Web.Pages.Product.Services;

public interface IProductService
{
    Task<Result<IEnumerable<CategoryModel>>> GetAllCatalogsAsync();

    Task<Result<ProductModel>> GetProductByIdAsync(Guid id);

    Task<Result<ProductModel>> CreateProductModel(ProductModel model);

    Task<Result<ProductModel>> UpdateProductModel(ProductModel model);

    Task<Result> DeleteProductAsync(Guid id);

    Task<Result<IList<ProductImageModel>>> GetProductImagesAsync(Guid id);
    
    Task<Result<ProductImageModel>> UploadImageAsync(MultipartFormDataContent form, Guid id);

    Task<Result> DeleteProductImageAsync(Guid id, DeleteProductImageModel model);
}