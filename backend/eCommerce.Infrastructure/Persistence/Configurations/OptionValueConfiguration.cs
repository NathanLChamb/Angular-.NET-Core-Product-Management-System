using eCommerce.Domain.Metadata;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eCommerce.Infrastructure.Persistence.Configurations
{
    public class OptionValueConfiguration : IEntityTypeConfiguration<OptionValue>
    {
        public void Configure(EntityTypeBuilder<OptionValue> builder)
        {
            builder.HasKey(ov => ov.Id);

            builder.Property(ov => ov.Value)
                .IsRequired()
                .HasMaxLength(50);

            builder.HasOne(ov => ov.Option)
                .WithMany(o => o.OptionValues)
                .HasForeignKey(ov => ov.OptionId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(ov => ov.ProductVariantOptionValues)
                .WithOne(pvov => pvov.OptionValue)
                .HasForeignKey(pvov => pvov.OptionValueId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
