using Microsoft.EntityFrameworkCore;
namespace EshopApi.Infrastructure;

public class EshopDbContext : DbContext
{
    public EshopDbContext(DbContextOptions<EshopDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(EshopDbContext).Assembly);
    }
}
