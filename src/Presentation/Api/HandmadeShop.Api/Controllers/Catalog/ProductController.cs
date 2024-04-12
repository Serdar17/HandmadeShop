using Asp.Versioning;
using AutoMapper;
using HandmadeShop.Api.Controllers.Catalog.Models;
using HandmadeShop.Common.Extensions;
using HandmadeShop.SharedModel.Catalogs.Models;
using HandmadeShop.UserCase.Catalog.Commands.CreateProduct;
using HandmadeShop.UserCase.Catalog.Commands.DeleteProduct;
using HandmadeShop.UserCase.Catalog.Commands.DeleteProductImage;
using HandmadeShop.UserCase.Catalog.Commands.UpdateProduct;
using HandmadeShop.UserCase.Catalog.Commands.UploadImages;
using HandmadeShop.UserCase.Catalog.Models;
using HandmadeShop.UserCase.Catalog.Queries.GetAllCatalogs;
using HandmadeShop.UserCase.Catalog.Queries.GetProductById;
using HandmadeShop.UserCase.Catalog.Queries.GetProductImages;
using HandmadeShop.UserCase.Catalog.Queries.GetProductInfo;
using HandmadeShop.UserCase.Catalog.Queries.GetProducts;
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
    /// Get products by query params
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpGet]
    [ProducesResponseType(typeof(PagedList<ProductModel>), 200)]
    public async Task<IResult> GetProductsAsync([FromQuery] ProductQueryRequest request)
    {
        var query = new GetProductsQuery(_mapper.Map<ProductQueryModel>(request));

        var result = await _sender.Send(query);
        
        return Results.Ok(result.Value);
    }

    /// <summary>
    /// Get product by id
    /// </summary>
    /// <param name="productId">Unique product id</param>
    /// <returns></returns>
    [HttpGet("{productId:guid}")]
    [ProducesResponseType(typeof(ProductModel), 200)]
    public async Task<IResult> GetProductByIdAsync([FromRoute] Guid productId)
    {
        var query = new GetProductByIdQuery(productId);

        var result = await _sender.Send(query);

        if (result.IsSuccess)
            return Results.Ok(result.Value);

        return result.ToProblemDetails();
    }

    /// <summary>
    /// Get product info by id
    /// </summary>
    /// <param name="productId">Unique product id</param>
    /// <returns>Product model</returns>
    [HttpGet("info/{productId}")]
    [ProducesResponseType(typeof(ProductInfoModel), 200)]
    public async Task<IResult> GetProductInfoAsync([FromRoute] Guid productId)
    {
        var query = new GetProductInfoQuery(productId);

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
    [ProducesResponseType(typeof(ProductModel), 200)]
    public async Task<IResult> CreateProductAsync([FromBody] CreateProductRequest request)
    {
        var command = new CreateProductCommand(_mapper.Map<CreateProductModel>(request));

        var result = await _sender.Send(command);

        if (result.IsSuccess)
            return Results.Ok(result.Value);

        return result.ToProblemDetails();
    }

    /// <summary>
    /// Update product
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPut]
    [ProducesResponseType(typeof(ProductModel), 200)]
    public async Task<IResult> UpdateProductAsync([FromBody] UpdateProductRequest request)
    {
        var command = new UpdateProductCommand(_mapper.Map<UpdateProductModel>(request));

        var result = await _sender.Send(command);

        if (result.IsSuccess)
            return Results.Ok(result.Value);

        return result.ToProblemDetails();
    }

    /// <summary>
    /// Delete product by id
    /// </summary>
    /// <param name="productId">Unique product id</param>
    /// <returns></returns>
    [HttpDelete("{productId:guid}")]
    [ProducesResponseType(204)]
    public async Task<IResult> DeleteProductAsync([FromRoute] Guid productId)
    {
        var command = new DeleteProductCommand(productId);

        var result = await _sender.Send(command);

        if (result.IsSuccess)
            return Results.NoContent();

        return result.ToProblemDetails();
    }

    /// <summary>
    /// Get product images by product id
    /// </summary>
    /// <param name="productId">Unique product id</param>
    /// <returns></returns>
    [HttpGet("images/{productId:guid}")]
    [ProducesResponseType(typeof(IEnumerable<ProductImageModel>), 200)]
    public async Task<IResult> GetProductImagesAsync([FromRoute] Guid productId)
    {
        var query = new GetProductImagesQuery(productId);

        var result = await _sender.Send(query);

        if (result.IsSuccess)
            return Results.Ok(result.Value);

        return result.ToProblemDetails();
    }

    /// <summary>
    /// Upload product images
    /// </summary>
    /// <param name="productId">Unique product id</param>
    /// <param name="request">List of product images</param>
    /// <returns></returns>
    [HttpPost("upload/images/{productId:guid}")]
    [ProducesResponseType(typeof(UploadedImageModel), 200)]
    public async Task<IResult> UploadImagesAsync([FromRoute] Guid productId, [FromForm] UploadImagesRequest request)
    {
        var command = new UploadImagesCommand(productId, _mapper.Map<UploadImagesModel>(request));

        var result = await _sender.Send(command);

        if (result.IsSuccess)
            return Results.Ok(result.Value);

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