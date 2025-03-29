using Moq;
using EshopApi.Domain.Interfaces;
using EshopApi.Application.Interfaces;
using Microsoft.Extensions.Logging;
using YourProject.WebApi.Api.Controllers;
using EshopApi.Shared.Dtos;
using Microsoft.AspNetCore.Mvc;
using EshopApi.WebApi.Api.Models;
using EshopApi.Domain.Entities;
using EshopApi.Shared.Exceptions;
using EshopApi.Shared.Models;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace EshopApi.Tests.Api.Controllers
{
    public class ProductsControllerTests
    {
        private readonly Mock<IProductRepository> _mockProductRepository;
        private readonly Mock<IProductService> _mockProductService;
        private readonly Mock<ILogger<ProductsController>> _mockLogger;
        private readonly ProductsController _controller;

        public ProductsControllerTests()
        {
            _mockProductRepository = new Mock<IProductRepository>();
            _mockProductService = new Mock<IProductService>();
            _mockLogger = new Mock<ILogger<ProductsController>>();
            _controller = new ProductsController(_mockProductRepository.Object, _mockProductService.Object, _mockLogger.Object);
        }

        [Fact]
        public async Task GetAllProducts_ReturnsOkResult_WithProductList()
        {
            // Arrange
            var mockProducts = new List<Product>
            {
                new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "Product 1",
                    PictureUri = "url",
                    Price = 10.0m,
                    Description = "Description 1"
                },
                new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "Product 2",
                    PictureUri = "url",
                    Price = 15.0m,
                    Description = "Description 2"
                }
            };

            _mockProductRepository.Setup(repo => repo.GetAllProductsQuery())
                .Returns(mockProducts.AsQueryable());

            // Act
            var result = await _controller.GetAllProducts();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnValue = Assert.IsType<List<ProductDto>>(okResult.Value);
            Assert.Equal(2, returnValue.Count);
        }

        [Fact]
        public async Task GetAllProducts_ReturnsOkResult_WithNoProducts()
        {
            // Arrange
            _mockProductRepository.Setup(repo => repo.GetAllProductsQuery())
                .Returns(new List<Product>().AsQueryable());

            // Act
            var result = await _controller.GetAllProducts();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnValue = Assert.IsType<string>(okResult.Value);
            Assert.Equal("No products found", returnValue);
        }

        [Fact]
        public async Task GetAllPaginatedProducts_ReturnsOkResult_WithPaginatedProducts()
        {
            // Arrange
            var queryStringParameters = new QueryStringParameters { PageNumber = 1, PageSize = 5 };

            var productsList = new List<ProductDto>(
                new List<ProductDto>
                {
                    new ProductDto
                    {
                        Id = Guid.NewGuid(),
                        Name = "Product 1",
                        PictureUri = "url",
                        Price = 10.0m },
                    new ProductDto
                    {
                        Id = Guid.NewGuid(),
                        Name = "Product 2",
                        PictureUri = "url",
                        Price = 15.0m
                    }
                }
            );

            int totalCount = 20;

            _mockProductService
                .Setup(service => service.GetPaginatedProductsAsync(queryStringParameters))
                .ReturnsAsync((productsList, totalCount));

            _controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext()
            };

            // Act
            var result = await _controller.GetAllPaginatedProducts(queryStringParameters);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnedProducts = Assert.IsType<List<ProductDto>>(okResult.Value);
            Assert.Equal(2, returnedProducts.Count);

            Assert.True(_controller.Response.Headers.ContainsKey("X-Pagination"));
            var paginationHeader = _controller.Response.Headers["X-Pagination"].ToString();
            Assert.False(string.IsNullOrEmpty(paginationHeader), "X-Pagination header is empty or null");
            PaginationMetadata? paginationData = null;

            if (!string.IsNullOrEmpty(paginationHeader))
            {
                paginationData = JsonConvert.DeserializeObject<PaginationMetadata>(paginationHeader);
            }

            Assert.NotNull(paginationData);
            Assert.Equal(20, paginationData.TotalCount);
            Assert.Equal(5, paginationData.PageSize);
            Assert.Equal(1, paginationData.CurrentPage);
            Assert.Equal(4, paginationData.TotalPages);
            Assert.True(paginationData.HasNext);
            Assert.False(paginationData.HasPrevious);
        }

        [Fact]
        public async Task GetProduct_ReturnsOkResult_WhenProductExists()
        {
            // Arrange
            var productId = Guid.NewGuid();
            var product = new Product
            {
                Id = productId,
                Name = "Sample Product",
                Price = 100.00m,
                Description = "Sample product description",
                PictureUri = "sample-image.jpg"
            };

            _mockProductRepository.Setup(repo => repo.GetProductByIdAsync(productId))
                .ReturnsAsync(product);

            // Act
            var result = await _controller.GetProduct(productId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnedProduct = Assert.IsType<ProductDto>(okResult.Value);
            Assert.Equal(productId, returnedProduct.Id);
            Assert.Equal("Sample Product", returnedProduct.Name);
        }

        [Fact]
        public async Task GetProduct_ReturnsNotFound_WhenProductDoesNotExist()
        {
            // Arrange
            var productId = Guid.NewGuid();
            _mockProductRepository.Setup(repo => repo.GetProductByIdAsync(productId))
                .ReturnsAsync((Product?)null);

            // Act
            var result = await _controller.GetProduct(productId);

            // Assert
            var notFoundResult = Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public async Task UpdateProductDescription_ReturnsUpdatedProduct_WhenProductExists()
        {
            // Arrange
            var productId = Guid.NewGuid();
            var newDescription = "Updated Description";
            var updateModel = new UpdateDescriptionModel { Description = newDescription };

            var product = new ProductDto
            {
                Id = productId,
                Name = "Product 1",
                Price = 100.0m,
                PictureUri = "http://example.com/product1.jpg",
                Description = newDescription
            };

            _mockProductService.Setup(service => service.UpdateProductDescriptionAsync(productId, newDescription))
                .ReturnsAsync(product);

            // Act
            var result = await _controller.UpdateProductDescription(productId, updateModel);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnValue = Assert.IsType<ProductDto>(okResult.Value);
            Assert.Equal(newDescription, returnValue.Description);
        }

        [Fact]
        public async Task UpdateProductDescription_ReturnsNotFound_WhenProductDoesNotExist()
        {
            // Arrange
            var productId = Guid.NewGuid();
            var updateModel = new UpdateDescriptionModel { Description = "Updated description" };

            _mockProductService.Setup(service => service.UpdateProductDescriptionAsync(productId, updateModel.Description))
                .ThrowsAsync(new NotFoundException("Product not found"));

            // Act
            var result = await _controller.UpdateProductDescription(productId, updateModel);

            // Assert
            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result.Result);
            Assert.Equal("Product not found", notFoundResult.Value);
        }
    }
}
