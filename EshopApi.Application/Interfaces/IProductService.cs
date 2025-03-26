using EshopApi.Domain.Entities;
using EshopApi.Shared.Dtos;

namespace EshopApi.Application.Interfaces
{
    public interface IProductService
    {
        Task<ProductDto> UpdateProductDescriptionAsync(Guid id, string description);
    }
}