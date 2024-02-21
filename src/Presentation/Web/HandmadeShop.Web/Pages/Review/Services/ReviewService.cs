using System.Net.Mime;
using System.Text;
using System.Text.Json;
using HandmadeShop.Domain.Common;
using HandmadeShop.SharedModel.Reviews.Models;
using HandmadeShop.Web.Extensions;

namespace HandmadeShop.Web.Pages.Review.Services;

public class ReviewService : IReviewService
{
    private static readonly string Root = $"{Settings.ApiRoot}/api/v1/reviews";
    private readonly HttpClient _httpClient;

    public ReviewService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<Result<IEnumerable<ReviewInfoModel>>> GetProductReviews(Guid productId)
    {
        var url = $"{Root}/{productId}";

        var response = await _httpClient.GetAsync(url);
        var content = await response.Content.ReadAsStringAsync();

        if (response.IsSuccessStatusCode)
        {
            var model = JsonSerializer.Deserialize<IEnumerable<ReviewInfoModel>>(content,
                       new JsonSerializerOptions { PropertyNameCaseInsensitive = true })
                   ?? Enumerable.Empty<ReviewInfoModel>();

            return Result<IEnumerable<ReviewInfoModel>>.Success(model);
        }

        return await response.Content.ToErrorAsync();
    }

    public async Task<Result<ReviewInfoModel>> AddReviewAsync(ReviewModel model)
    {
        var json = JsonSerializer.Serialize(model);
        var data = new StringContent(json, Encoding.UTF8, MediaTypeNames.Application.Json);

        var response = await _httpClient.PostAsync(Root, data);
        var content = await response.Content.ReadAsStringAsync();

        if (response.IsSuccessStatusCode)
        {
            return JsonSerializer.Deserialize<ReviewInfoModel>(content,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true }) ?? new ReviewInfoModel();
        }

        return await response.Content.ToErrorAsync();
    }

    public async Task<Result> RemoveReviewAsync(Guid reviewId)
    {
        var url = $"{Settings.WebRoot}/{reviewId}";

        var response = await _httpClient.DeleteAsync(url);
        
        if (response.IsSuccessStatusCode)
        {
            return Result.Success();
        }

        return await response.Content.ToErrorAsync();
    }

    public async Task<Result> AddFavoriteAsync(AddFavoriteModel model)
    {
        var url = $"{Root}/favorites";

        var json = JsonSerializer.Serialize(model);
        var data = new StringContent(json, Encoding.UTF8, MediaTypeNames.Application.Json);
        
        var response = await _httpClient.PostAsync(url, data);
        var content = await response.Content.ReadAsStringAsync();

        if (response.IsSuccessStatusCode)
            return Result.Success();

        return await response.Content.ToErrorAsync();
    }

    public async Task<Result> RemoveFavoriteAsync(RemoveFavoriteModel model)
    {
        var url = $"{Root}/favorites";
        
        var json = JsonSerializer.Serialize(model);
        var data = new StringContent(json, Encoding.UTF8, MediaTypeNames.Application.Json);

        var message = new HttpRequestMessage(HttpMethod.Delete, url) {Content = data};
        var response = await _httpClient.SendAsync(message);

        if (response.IsSuccessStatusCode)
        {
            return Result.Success();
        }

        return await response.Content.ToErrorAsync();
    }
}