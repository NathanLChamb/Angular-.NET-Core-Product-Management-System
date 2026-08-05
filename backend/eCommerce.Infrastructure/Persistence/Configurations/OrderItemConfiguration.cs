using eCommerce.Domain.Order;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eCommerce.Infrastructure.Persistence.Configurations
{
    public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder.HasKey(oi => oi.Id);

            builder.Property(oi => oi.OrderId)
                .IsRequired();

            builder.HasOne(oi => oi.Order)
                .WithMany(o => o.OrderItems)
                .HasForeignKey(oi => oi.OrderId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Property(oi => oi.ProductVariantId)
                .IsRequired();

            builder.HasOne(oi => oi.ProductVariant)
                .WithMany()
                .HasForeignKey(oi => oi.ProductVariantId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Property(oi => oi.ProductName)
                .IsRequired()
                .HasMaxLength(500);

            builder.Property(oi => oi.Sku)
                .IsRequired()
                .HasMaxLength(64);

            builder.Property(oi => oi.PriceAtPurchase)
                .HasPrecision(18, 2)
                .IsRequired();

            builder.Property(oi => oi.Quantity)
                .IsRequired();
        }
    }
}
