using eCommerce.Domain.Metadata;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eCommerce.Infrastructure.Persistence.Configurations
{
    public class OptionConfiguration : IEntityTypeConfiguration<Option>
    {
        public void Configure(EntityTypeBuilder<Option> builder)
        {
            builder.HasKey(o => o.Id);

            builder.HasIndex(o => o.Name)
                .IsUnique();

            builder.Property(o => o.Name)
                .IsRequired()
                .HasMaxLength(50);

            builder.HasMany(o => o.OptionValues)
                .WithOne(ov => ov.Option)
                .HasForeignKey(ov => ov.OptionId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(o => o.ProductOptions)
                .WithOne(po => po.Option)
                .HasForeignKey(po => po.OptionId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
