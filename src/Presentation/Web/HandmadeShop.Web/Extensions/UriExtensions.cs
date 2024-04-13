using System.Web;
using HandmadeShop.SharedModel.Catalogs.Models;

namespace HandmadeShop.Web.Extensions;

public static class UriExtensions
{
    /// <summary>
    /// Add the specified parameter to the Query String.
    /// </summary>
    /// <param name="url"></param>
    /// <param name="paramName">Name of the parameter to add.</param>
    /// <param name="paramValue">Value for the parameter to add.</param>
    /// <returns>Url with added parameter.</returns>
    public static Uri AddParameter(this Uri url, string paramName, string paramValue)
    {
        var uriBuilder = new UriBuilder(url);
        var query = HttpUtility.ParseQueryString(uriBuilder.Query);
        query[paramName] = paramValue;
        uriBuilder.Query = query.ToString();

        return uriBuilder.Uri;
    }

    public static string GetUrlWithParams(this Uri uri, ProductQueryModel model)
    {
        return uri.AddParameter("catalogName", model.CatalogName)
            .AddParameter("pageSize", model.PageSize.ToString())
            .AddParameter("page", model.Page.ToString())
            .AddParameter("sortOrder", model.SortOrder)
            .AddParameter("sortColumn", model.SortColumn)
            .AddParameter("search", model.Search)
            .AddParameter("priceFrom", model.PriceFrom.ToString())
            .AddParameter("priceTo", model.PriceTo.ToString())
            .AddParameter("isFavorite", model.IsFavorite.ToString())
            .ToString();
    }
}