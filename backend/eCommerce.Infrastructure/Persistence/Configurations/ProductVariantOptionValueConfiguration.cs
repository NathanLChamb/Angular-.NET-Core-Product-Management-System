using eCommerce.Domain.Product;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eCommerce.Infrastructure.Persistence.Configurations
{
    public class ProductVariantOptionValueConfiguration : IEntityTypeConfiguration<ProductVariantOptionValue>
    {
        public void Configure(EntityTypeBuilder<ProductVariantOptionValue> builder)
        {
            builder.HasKey(pvov => new
            {
                pvov.ProductVariantId,
                pvov.OptionValueId
            });

            builder.HasOne(pvov => pvov.ProductVariant)
                .WithMany(pv => pv.ProductVariantOptionValues)
                .HasForeignKey(pvov => pvov.ProductVariantId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(pvov => pvov.OptionValue)
                .WithMany(ov => ov.ProductVariantOptionValues)
                .HasForeignKey(pvov => pvov.OptionValueId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
