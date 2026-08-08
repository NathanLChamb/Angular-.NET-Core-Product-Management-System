using eCommerce.Domain.Metadata;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eCommerce.Infrastructure.Persistence.Configurations
{
    public class CatalogAttributeConfiguration : IEntityTypeConfiguration<CatalogAttribute>
    {
        public void Configure(EntityTypeBuilder<CatalogAttribute> builder)
        {
            builder.HasKey(ca => ca.Id);

            builder.Property(ca => ca.Name)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(ca => ca.Value)
                .IsRequired()
                .HasMaxLength(50);
        }
    }
}
