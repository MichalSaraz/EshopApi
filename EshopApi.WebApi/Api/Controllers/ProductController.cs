using EshopApi.Domain.Interfaces;
using EshopApi.WebApi.Api.Models;
using Microsoft.AspNetCore.Mvc;
using EshopApi.Shared.Dtos;
using EshopApi.Application.Interfaces;
using System.ComponentModel.DataAnnotations;
using EshopApi.Shared.Exceptions;
using Microsoft.EntityFrameworkCore;


namespace YourProject.WebApi.Api.Controllers
{
    [ApiController]
    [Route("api/products")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductRepository _productRepository;
        private readonly IProductService _productService;
        private readonly ILogger<ProductsController> _logger;

        public ProductsController(
            IProductRepository productRepository,
            IProductService productService,
            ILogger<ProductsController> logger)
        {
            _productRepository = productRepository;
            _productService = productService;
            _logger = logger;
        }

        [HttpGet("all-products")]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetAllProducts()
        {
            var products = await _productRepository.GetAllProductsQuery()
                .Select(p => new ProductDto
                {
                    Id = p.Id,
                    Name = p.Name,
                    PictureUri = p.PictureUri,
                    Price = p.Price,
                    Description = p.Description
                })
                .ToListAsync();

            if (!products.Any())
            {
                return Ok("No products found");
            }

            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDto>> GetProduct(Guid id)
        {
            var product = await _productRepository.GetProductByIdAsync(id);

            if (product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }

        [HttpPatch("{id}/description")]
        public async Task<ActionResult<ProductDto>> UpdateProductDescription(Guid id, [FromBody] UpdateDescriptionModel model)
        {
            try
            {
                var updatedProduct = await _productService.UpdateProductDescriptionAsync(id, model.Description);
                return Ok(updatedProduct);
            }
            catch (ValidationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while updating product description");
                return StatusCode(500, "An error occurred while updating product description");
            }
        }
    }
}