using eCommerce.Domain.Metadata;
using eCommerce.Domain.Product;
using Microsoft.EntityFrameworkCore;

namespace eCommerce.Application.Interfaces
{
    public interface IeCommerceContext
    {
        DbSet<Product> Products { get; }
        DbSet<Category> Categories { get; }
        DbSet<Option> Options { get; }
        DbSet<ProductVariant> ProductVariants { get; }
        DbSet<OptionValue> OptionValues { get; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
