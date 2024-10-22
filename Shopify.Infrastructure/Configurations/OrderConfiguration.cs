using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shopify.Domain.Orders;
using Shopify.Domain.Users;

namespace Shopify.Infrastructure.Configurations
{
    public sealed class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.ToTable("Orders");

            builder.HasKey(o => o.Id);

            builder.HasOne<User>()
                  .WithMany()
                  .HasForeignKey(o => o.UserId)
                  .OnDelete(DeleteBehavior.Cascade);

            builder.Property(o => o.TotalCost)
                .IsRequired()
                .HasColumnType("decimal(18,2)");

            builder.Property(o => o.CreatedDate)
                .IsRequired();

            builder.Property(o => o.UpdatedDate)
                .IsRequired();

            builder.Property(o => o.DeliveryTime)
                .IsRequired();

            builder.HasMany(o => o.OrderDetails)
                .WithOne()
                .HasForeignKey("OrderId")
                .OnDelete(DeleteBehavior.Cascade);

            builder.Property(p => p.DeliveryAddress)
              .IsRequired()
              .HasMaxLength(500)
              .HasColumnName("DeliveryAddress")
              .HasConversion(
                  deliveryAddress => deliveryAddress.Value,
                  value => new DeliveryAddress(value)
              );

        }
    }
}