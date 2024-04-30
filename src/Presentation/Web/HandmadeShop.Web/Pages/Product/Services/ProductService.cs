using System.Net.Mime;
using System.Text;
using System.Text.Json;
using HandmadeShop.Domain.Common;
using HandmadeShop.SharedModel.Catalogs.Models;
using HandmadeShop.Web.Extensions;
using HandmadeShop.Web.Pages.Product.Models;

namespace HandmadeShop.Web.Pages.Product.Services;

public class ProductService(IHttpClientFactory factory) : IProductService
{
    private readonly HttpClient _httpClient = factory.CreateClient(Settings.Api);

    public async Task<Result<IEnumerable<CategoryModel>>> GetAllCatalogsAsync()
    {
        var url = $"{Settings.ApiRoot}/api/v1/products/catalogs";

        var response = await _httpClient.GetAsync(url);
        var content = await response.Content.ReadAsStringAsync();

        if (response.IsSuccessStatusCode)
        {
            var model =  JsonSerializer.Deserialize<IEnumerable<CategoryModel>>(content,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true }) ?? new List<CategoryModel>();

            return Result<IEnumerable<CategoryModel>>.Success(model);
        }
        
        return content.ToError();
    }

    public async Task<Result<ProductModel>> GetProductByIdAsync(Guid id)
    {
        var url = $"{Settings.ApiRoot}/api/v1/products/{id}";

        var response = await _httpClient.GetAsync(url);
        var content = await response.Content.ReadAsStringAsync();

        if (response.IsSuccessStatusCode)
        {
            return JsonSerializer.Deserialize<ProductModel>(content,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true }) 
                   ?? new ProductModel
                   {
                       Name = string.Empty,
                       Description = string.Empty
                   };

        }
        
        return content.ToError();
    }

    public async Task<Result<PagedList<ProductModel>?>> GetProductsByQueryAsync(ProductQueryModel query)
    {
        var url = new Uri($"{Settings.ApiRoot}/api/v1/products").GetUrlWithParams(query);

        var response = await _httpClient.GetAsync(url);
        var content = await response.Content.ReadAsStringAsync();
        
        if (response.IsSuccessStatusCode)
        {
            return JsonSerializer.Deserialize<PagedList<ProductModel>>(content,
                       new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }

        return content.ToError();
    }

    public async Task<Result<ProductInfoModel?>> GetProductInfoModel(Guid id)
    {
        var url = $"{Settings.ApiRoot}/api/v1/products/info/{id}";

        var response = await _httpClient.GetAsync(url);
        var content = await response.Content.ReadAsStringAsync();

        if (response.IsSuccessStatusCode)
        {
            return JsonSerializer.Deserialize<ProductInfoModel>(content,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }

        return content.ToError();
    }

    public async Task<Result<ProductModel>> CreateProductModel(ProductModel model)
    {
        var url = $"{Settings.ApiRoot}/api/v1/products";

        var json = JsonSerializer.Serialize(model);
        var data = new StringContent(json, Encoding.UTF8, MediaTypeNames.Application.Json);

        var response = await _httpClient.PostAsync(url, data);
        var content = await response.Content.ReadAsStringAsync();
        
        if (response.IsSuccessStatusCode)
        {
            return JsonSerializer.Deserialize<ProductModel>(content,
                       new JsonSerializerOptions { PropertyNameCaseInsensitive = true })
                   ?? new ProductModel()
                   {
                       Name=string.Empty,
                       Description = string.Empty
                   };
        }

        return content.ToError();
    }

    public async Task<Result<ProductModel>> UpdateProductModel(ProductModel model)
    {
        var url = $"{Settings.ApiRoot}/api/v1/products";

        var json = JsonSerializer.Serialize(model);
        var data = new StringContent(json, Encoding.UTF8, MediaTypeNames.Application.Json);

        var response = await _httpClient.PutAsync(url, data);
        var content = await response.Content.ReadAsStringAsync();
        
        if (response.IsSuccessStatusCode)
        {
            return JsonSerializer.Deserialize<ProductModel>(content,
                       new JsonSerializerOptions { PropertyNameCaseInsensitive = true })
                   ?? new ProductModel
                   {
                       Name = string.Empty,
                       Description = string.Empty
                   };
        }

        return content.ToError();
    }

    public async Task<Result> DeleteProductAsync(Guid id)
    {
        var url = $"{Settings.ApiRoot}/api/v1/products/{id}";

        var response = await _httpClient.DeleteAsync(url);
        var content = await response.Content.ReadAsStringAsync();
        
        if (response.IsSuccessStatusCode)
            return Result.Success();

        return content.ToError();
    }

    public async Task<Result<IList<ProductImageModel>>> GetProductImagesAsync(Guid id)
    {
        var url = $"{Settings.ApiRoot}/api/v1/products/images/{id}";

        var response = await _httpClient.GetAsync(url);
        var content = await response.Content.ReadAsStringAsync();

        if (response.IsSuccessStatusCode)
        {
            var model = JsonSerializer.Deserialize<IList<ProductImageModel>>(content,
                       new JsonSerializerOptions { PropertyNameCaseInsensitive = true }) ??
                   new List<ProductImageModel>();

            return Result<IList<ProductImageModel>>.Success(model);
        }

        return content.ToError();
    }

    public async Task<Result<ProductImageModel>> UploadImageAsync(MultipartFormDataContent form, Guid id)
    {
        var url = $"{Settings.ApiRoot}/api/v1/products/upload/images/{id}";
        
        var response = await _httpClient.PostAsync(url, form);
        
        var content = await response.Content.ReadAsStringAsync();

        if (response.IsSuccessStatusCode)
        {
            return JsonSerializer.Deserialize<ProductImageModel>(content,
                       new JsonSerializerOptions { PropertyNameCaseInsensitive = true })
                   ?? new ProductImageModel();
        }

        return content.ToError();
    }

    public async Task<Result> DeleteProductImageAsync(Guid id, DeleteProductImageModel model)
    {
        var url = $"{Settings.ApiRoot}/api/v1/products/delete/images/{id}";

        var json = JsonSerializer.Serialize(model);
        var data = new StringContent(json, Encoding.UTF8, MediaTypeNames.Application.Json);

        var message = new HttpRequestMessage(HttpMethod.Delete, url) {Content = data};
        var response = await _httpClient.SendAsync(message);
        var content = await response.Content.ReadAsStringAsync();

        if (response.IsSuccessStatusCode)
        {
            return Result.Success();
        }

        return content.ToError();
    }
}