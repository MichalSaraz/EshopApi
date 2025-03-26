using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EshopApi.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EshopApi.Infrastructure.Data.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Products");
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(100)
                .IsUnicode(true);

            builder.Property(e => e.Description)
                .IsRequired()
                .HasMaxLength(500)
                .IsUnicode(true);

            builder.Property(e => e.Price)
                .IsRequired()
                .HasColumnType("decimal(18,2)");

            builder.Property(e => e.PictureUri)
                .IsRequired()
                .HasMaxLength(200)
                .IsUnicode(true);

            builder.HasIndex(e => e.Name);
            builder.HasIndex(e => e.Price);
        }
    }
}