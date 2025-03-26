using System.ComponentModel.DataAnnotations.Schema;
using EshopApi.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace EshopApi.Infrastructure;

public class EshopDbContext : DbContext
{
    private readonly IConfiguration _configuration;

    public EshopDbContext(DbContextOptions<EshopDbContext> options, IConfiguration configuration) : base(options)
    {
        _configuration = configuration;
    }
   
    public DbSet<Product> Products { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        options.UseSqlServer(_configuration.GetConnectionString("MasterConnection"));
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(EshopDbContext).Assembly);
    }
}
