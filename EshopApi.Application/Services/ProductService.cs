using System.ComponentModel.DataAnnotations;
using EshopApi.Application.Interfaces;
using EshopApi.Domain.Interfaces;
using EshopApi.Shared.Dtos;
using EshopApi.Shared.Exceptions;
using EshopApi.Shared.Models;

public class ProductService : IProductService
{
    private readonly IProductRepository _productRepository;

    public ProductService(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<ProductDto> UpdateProductDescriptionAsync(Guid id, string description)
    {
        if (string.IsNullOrWhiteSpace(description))
        {
            throw new ValidationException("Description is required");
        }

        var product = await _productRepository.GetProductByIdAsync(id);

        if (product == null)
        {
            throw new NotFoundException("Product not found");
        }

        product.Description = description;
        await _productRepository.UpdateProductAsync(product);

        return new ProductDto
        {
            Id = product.Id,
            Name = product.Name,
            Description = product.Description,
            Price = product.Price,
            PictureUri = product.PictureUri
        };
    }

    public async Task<PagedList<ProductDto>> GetPaginatedProductsAsync(QueryStringParameters parameters)
    {
        var query = _productRepository.GetAllProductsQuery();

        if (!parameters.IsValid(out string? errorMessage))
        {
            throw new ArgumentException(errorMessage);
        }

        var pagedProducts = await Task.Run(() =>
            PagedList<ProductDto>.ToPagedList(
                query.Select(p => new ProductDto
                {
                    Id = p.Id,
                    Name = p.Name,
                    PictureUri = p.PictureUri,
                    Price = p.Price,
                    Description = p.Description
                }),
                parameters.PageNumber,
                parameters.PageSize
            )
        );

        return pagedProducts;
    }
}