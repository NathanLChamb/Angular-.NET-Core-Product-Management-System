using Microsoft.EntityFrameworkCore;
using eCommerce.Domain.Metadata;
using eCommerce.Domain.Product;
using eCommerce.Application.Interfaces;

namespace eCommerce.Infrastructure.Persistence
{
    public class eCommerceContext : DbContext, IeCommerceContext
    {
        public eCommerceContext(DbContextOptions<eCommerceContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(eCommerceContext).Assembly);

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Option> Options { get; set; }
        public DbSet<OptionValue> OptionValues { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }
        public DbSet<ProductOption> ProductOptions { get; set; }
        public DbSet<ProductVariant> ProductVariants { get; set; }
        public DbSet<ProductVariantOptionValue> ProductVariantOptionValues { get; set; }
    }
}