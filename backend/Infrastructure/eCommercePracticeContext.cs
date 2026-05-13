using eCommercePractice4.Domain.Metadata;
using eCommercePractice4.Domain.Product;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace eCommercePractice4.Infrastructure
{
    public class eCommercePracticeContext : DbContext
    {
        public eCommercePracticeContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProductCategory>().HasKey(pc => new
            {
                pc.ProductId,
                pc.CategoryId
            });

            modelBuilder.Entity<ProductCategory>()
                .HasOne(pc => pc.Product)
                .WithMany(p => p.ProductCategories)
                .HasForeignKey(pc => pc.ProductId)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<ProductCategory>()
                .HasOne(pc => pc.Category)
                .WithMany(c => c.ProductCategories)
                .HasForeignKey(pc => pc.CategoryId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ProductOption>().HasKey(po => new
            {
                po.ProductId,
                po.OptionId
            });

            modelBuilder.Entity<ProductOption>()
                .HasOne(po => po.Product)
                .WithMany(p => p.ProductOptions)
                .HasForeignKey(po => po.ProductId)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<ProductOption>()
                .HasOne(po => po.Option)
                .WithMany(p => p.ProductOptions)
                .HasForeignKey(po => po.OptionId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ProductVariantOptionValue>().HasKey(pvov => new
            {
                pvov.ProductVariantId,
                pvov.OptionValueId
            });

            modelBuilder.Entity<ProductVariantOptionValue>()
                .HasOne(pvov => pvov.ProductVariant)
                .WithMany(pv => pv.ProductVariantOptionValues)
                .HasForeignKey(pvov => pvov.ProductVariantId)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<ProductVariantOptionValue>()
                .HasOne(pvov => pvov.OptionValue)
                .WithMany(ov => ov.ProductVariantOptionValues)
                .HasForeignKey(pvov => pvov.OptionValueId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<OptionValue>()
                .HasOne(ov => ov.Option)
                .WithMany(o => o.OptionValues)
                .HasForeignKey(ov => ov.OptionId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ProductVariant>()
                .HasOne(pv => pv.Product)
                .WithMany(p => p.ProductVariants)
                .HasForeignKey(pv => pv.ProductId)
                .OnDelete(DeleteBehavior.Cascade);


            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Option> Options { get; set; }
        public DbSet<OptionValue> OptionValues { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductVariant> ProductVariants { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }
        public DbSet<ProductOption> ProductOptions { get; set; }
        public DbSet<ProductVariantOptionValue> ProductVariantOptionValues { get; set; }

    }

    public class eCommercePracticeContextFactory : IDesignTimeDbContextFactory<eCommercePracticeContext>
    {
        public eCommercePracticeContext CreateDbContext(string[] args)
        {
            IConfiguration config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false)
                .Build();

            var optionsBuilder = new DbContextOptionsBuilder<eCommercePracticeContext>();
            var connectionString = config.GetConnectionString("DefaultConnectionString");
            optionsBuilder.UseSqlServer(connectionString);

            return new eCommercePracticeContext(optionsBuilder.Options);
        }
    }
}
