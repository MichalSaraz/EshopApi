using EshopApi.Domain.Entities;
using EshopApi.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EshopApi.Infrastructure.Data.Repositories
{
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

        public async Task<IEnumerable<Product>> GetProductsAsync()
        {
            var productsQuery = _context.Products;
            var products = await productsQuery.ToListAsync();
            return products;
        }
    }
}