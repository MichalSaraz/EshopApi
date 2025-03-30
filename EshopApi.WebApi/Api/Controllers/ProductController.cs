using EshopApi.Domain.Interfaces;
using EshopApi.WebApi.Api.Models;
using Microsoft.AspNetCore.Mvc;
using EshopApi.Shared.Dtos;
using EshopApi.Application.Interfaces;
using EshopApi.Shared.Exceptions;
using Newtonsoft.Json;
using EshopApi.Shared.Models;
using Asp.Versioning;
using EshopApi.Shared.Extensions;

namespace YourProject.WebApi.Api.Controllers
{
    /// <summary>
    /// Controller for managing products in the API.
    /// </summary>
    /// <remarks>
    /// Supports multiple API versions:
    /// - Version 1: Basic product retrieval and update operations.
    /// - Version 2: Includes paginated product retrieval.
    /// </remarks>
    [ApiController]
    [ApiVersion(1)]
    [ApiVersion(2)]
    [Route("api/v{v:apiVersion}/products")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductRepository _productRepository;
        private readonly IProductService _productService;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProductsController"/> class.
        /// </summary>
        /// <param name="productRepository">The repository for managing product data.</param>
        /// <param name="productService">The service for handling product-related business logic.</param>
        public ProductsController(
            IProductRepository productRepository,
            IProductService productService)
        {
            _productRepository = productRepository;
            _productService = productService;
        }

        /// <summary>
        /// Retrieves all products from the repository.
        /// </summary>
        /// <remarks>API Version: v1</remarks>
        /// <returns>
        /// An <see cref="ActionResult"/> containing a collection of <see cref="ProductDto"/> objects
        /// or a message if no products are available.
        /// </returns>
        /// <response code="200">Returns the list of products or a message if no products are found.</response>
        [HttpGet("all-products")]
        [MapToApiVersion(1)]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetAllProducts()
        {
            var products = await _productRepository.GetAllProductsQuery().ToListAsyncSafe();

            if (!products.Any())
            {
                return Ok("No products found");
            }

            var productDtos = products.Select(p => new ProductDto
            {
                Id = p.Id,
                Name = p.Name,
                PictureUri = p.PictureUri,
                Price = p.Price,
                Description = p.Description
            }).ToList();

            return Ok(productDtos);
        }

        /// <summary>
        /// Retrieves a paginated list of all products.
        /// </summary>
        /// <remarks>API Version: v2</remarks>
        /// <param name="parameters">The query string parameters for pagination, including page number and page size.</param>
        /// <returns>
        /// An <see cref="ActionResult"/> containing a paginated list of <see cref="ProductDto"/> objects 
        /// and pagination metadata in the response headers.
        /// </returns>
        /// <response code="200">Returns the paginated list of products.</response>
        /// <response code="400">Returns an error if the provided query parameters are invalid.</response>
        [HttpGet("all-paginated-products")]
        [MapToApiVersion(2)]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetAllPaginatedProducts([FromQuery] QueryStringParameters parameters)
        {
            if (parameters == null)
            {
                return BadRequest("Query parameters cannot be null");
            }

            if (!parameters.IsValid(out string? errorMessage))
            {
                return BadRequest(new { error = errorMessage });
            }

            var (products, totalCount) = await _productService.GetPaginatedProductsAsync(parameters);

            if (products == null)
            {
                return NotFound("No products found");
            }

            int totalPages = (int)Math.Ceiling((double)totalCount / parameters.PageSize);
            bool hasNext = parameters.PageNumber < totalPages;
            bool hasPrevious = parameters.PageNumber > 1;

            Response.Headers.Append("X-Pagination",
                JsonConvert.SerializeObject(new
                {
                    totalCount,
                    pageSize = parameters.PageSize,
                    currentPage = parameters.PageNumber,
                    totalPages,
                    hasNext,
                    hasPrevious
                }));

            return Ok(products);
        }

        /// <summary>
        /// Retrieves a product by its unique identifier.
        /// </summary>
        /// <remarks>API Version: v1</remarks>
        /// <param name="id">The unique identifier of the product.</param>
        /// <returns>
        /// An <see cref="ActionResult{T}"/> containing the product details as a <see cref="ProductDto"/> 
        /// if found, or a <see cref="NotFoundResult"/> if the product does not exist.
        /// </returns>
        /// <response code="200">Returns the product details.</response>
        /// <response code="404">If the product is not found.</response>
        [HttpGet("{id}")]
        [MapToApiVersion(1)]
        public async Task<ActionResult<ProductDto>> GetProduct(Guid id)
        {
            var product = await _productRepository.GetProductByIdAsync(id);

            if (product == null)
            {
                return NotFound();
            }

            var productDto = new ProductDto
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                Description = product.Description,
                PictureUri = product.PictureUri
            };

            return Ok(productDto);
        }

        /// <summary>
        /// Updates the description of a product identified by its unique ID.
        /// </summary>
        /// <remarks>API Version: v1</remarks>
        /// <param name="id">The unique identifier of the product to update.</param>
        /// <param name="model">The model containing the new description for the product.</param>
        /// <returns>
        /// An <see cref="ActionResult{T}"/> containing the updated product details if successful,
        /// or an appropriate error response if the product is not found or an error occurs.
        /// </returns>
        /// <response code="200">Returns the updated product details.</response>
        /// <response code="404">If the product with the specified ID is not found.</response>
        /// <response code="500">If an unexpected error occurs during the update process.</response>
        [HttpPatch("{id}/description")]
        [MapToApiVersion(1)]
        public async Task<ActionResult<ProductDto>> UpdateProductDescription(Guid id, [FromBody] UpdateDescriptionModel model)
        {
            try
            {
                var updatedProduct = await _productService.UpdateProductDescriptionAsync(id, model.Description);
                return Ok(updatedProduct);
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
