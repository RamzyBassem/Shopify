using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shopify.Domain.Products;

namespace Shopify.Infrastructure.Configurations
{
    public sealed class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Products");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(100)
                .HasColumnName("Name")
                .HasConversion(
                    name => name.Value,
                    value => new Name(value)
                );

            builder.HasIndex(p => p.Name).IsUnique();

            builder.Property(p => p.Description)
                .IsRequired()
                .HasMaxLength(500)
                .HasColumnName("Description")
                .HasConversion(
                    description => description.Value,
                    value => new Description(value)
                );

            builder.Property(p => p.Price)
                .IsRequired()
                .HasColumnType("decimal(18,2)")
                .HasColumnName("Price");

            builder.Property(p => p.Merchant)
                .IsRequired()
                .HasMaxLength(100)
                .HasColumnName("Merchant")
                .HasConversion(
                    merchant => merchant.Value,
                    value => new Merchant(value)
                );

            builder.Property(p => p.ImagePath)
               .HasMaxLength(500)
               .HasColumnName("ImagePath")
               .HasConversion<string?>(
                   v => v != null ? v.Value : null, 
            v => string.IsNullOrEmpty(v) ? null : new ImagePath(v) 
               );
        }

    }
}
