using EshopApi.Domain.Entities;
using EshopApi.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EshopApi.Infrastructure.Data.Repositories
{
    /// <summary>
    /// Repository for managing product data.
    /// </summary>
    public class ProductRepository : IProductRepository
    {
        private readonly EshopDbContext _context;

        public ProductRepository(EshopDbContext context)
        {
            _context = context;
        }

        public async Task<Product?> GetProductByIdAsync(Guid id)
        {
            var productQuery = _context.Products.Where(p => p.Id == id);
            var product = await productQuery.FirstOrDefaultAsync();
            return product ?? null;
        }

        public IQueryable<Product> GetAllProductsQuery()
        {
            return _context.Products.AsQueryable();
        }

        public async Task<Product> UpdateProductAsync(Product product)
        {
            _context.Entry(product).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return product;
        }
    }
}