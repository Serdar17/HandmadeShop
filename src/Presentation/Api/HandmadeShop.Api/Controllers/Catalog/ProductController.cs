using Asp.Versioning;
using AutoMapper;
using HandmadeShop.Api.Controllers.Catalog.Models;
using HandmadeShop.Common.Extensions;
using HandmadeShop.UserCase.Catalog.Commands.CreateProduct;
using HandmadeShop.UserCase.Catalog.Commands.DeleteProductImage;
using HandmadeShop.UserCase.Catalog.Commands.UploadImages;
using HandmadeShop.UserCase.Catalog.Models;
using HandmadeShop.UserCase.Catalog.Queries.GetAllCatalogs;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HandmadeShop.Api.Controllers.Catalog;

/// <summary>
/// Accounts controller
/// </summary>
/// <response code="400">Bad Request</response>;
/// <response code="401">Unauthorized</response>;
/// <response code="403">Forbidden</response>;
/// <response code="404">Not Found</response>;
/// <response code="409">Conflict</response>;
[Route("api/v{version:apiVersion}/products")]
[Produces("application/json")]
[ApiController]
[ApiVersion("1.0")]
[Authorize]
public class ProductController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly ISender _sender;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="mapper"></param>
    /// <param name="sender"></param>
    public ProductController(IMapper mapper, ISender sender)
    {
        _mapper = mapper;
        _sender = sender;
    }

    /// <summary>
    /// Get all catalogs
    /// </summary>
    /// <returns></returns>
    [HttpGet("catalogs")]
    [AllowAnonymous]
    [ProducesResponseType(typeof(IEnumerable<CatalogModel>), 200)]
    public async Task<IResult> GetAllCatalogsAsync()
    {
        var query = new GetAllCatalogsQuery();

        var result = await _sender.Send(query);

        if (result.IsSuccess)
            return Results.Ok(result.Value);

        return result.ToProblemDetails();
    }

    /// <summary>
    /// Create product for user
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<IResult> CreateProductAsync([FromBody] CreateProductRequest request)
    {
        var command = new CreateProductCommand(_mapper.Map<CreateProductModel>(request));

        var result = await _sender.Send(command);

        if (result.IsSuccess)
            return Results.Ok();

        return result.ToProblemDetails();
    }

    /// <summary>
    /// Upload product images
    /// </summary>
    /// <param name="productId">Unique product id</param>
    /// <param name="request">List of product images</param>
    /// <returns></returns>
    [HttpPost("upload/images/{productId:guid}")]
    public async Task<IResult> UploadImagesAsync([FromRoute] Guid productId, [FromForm] UploadImagesRequest request)
    {
        var command = new UploadImagesCommand(productId, _mapper.Map<UploadImagesModel>(request));

        var result = await _sender.Send(command);

        if (result.IsSuccess)
            return Results.Ok();

        return result.ToProblemDetails();
    }

    /// <summary>
    /// Delete product image
    /// </summary>
    /// <param name="productId">Unique product id</param>
    /// <param name="request">Delete product image request</param>
    /// <returns></returns>
    [HttpDelete("delete/images/{productId:guid}")]
    public async Task<IResult> DeleteProductImageAsync([FromRoute] Guid productId,
        [FromBody] DeleteProductImagesRequest request)
    {
        var command = new DeleteProductImageCommand(productId, _mapper.Map<DeleteProductImageModel>(request));

        var result = await _sender.Send(command);

        if (result.IsSuccess)
            return Results.Ok();

        return result.ToProblemDetails();
    }
}