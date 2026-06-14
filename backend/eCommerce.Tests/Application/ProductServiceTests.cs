using eCommerce.Application.DTOs.Product;
using eCommerce.Application.Services;
using eCommerce.Tests.Infrastructure;
using FluentAssertions;

namespace eCommerce.Tests.Application
{
    [Collection("Database Collection")]
    public class ProductServiceTests
    {
        private readonly PostgresContainerFixture _fixture;

        public ProductServiceTests(PostgresContainerFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public async Task CreateProduct_ShouldPersistProductWithVariants()
        {
            await _fixture.ResetDatabase();

            using var context = _fixture.CreateDbContext();
            var service = new ProductService(context);

            var dto = new CreateProductDto
            {
                Name = "Phone",
                Description = "Smartphone",
                CategoryIds = new List<int>(),
                OptionIds = new List<int>(),
                ProductVariants = new List<CreateProductVariantDto>
                {
                    new CreateProductVariantDto
                    {
                        Sku = "PHONE-BLACK-64",
                        Price = 999,
                        StockQuantity = 10,
                        OptionValueIds = new List<int>()
                    }
                }
            };

            var result = await service.CreateProductAsync(dto);

            result.Should().NotBeNull();
            result.Id.Should().BeGreaterThan(0);
            result.ProductVariants.Should().HaveCount(1);
        }

        [Fact]
        public async Task UpdateProduct_ShouldRemoveDeletedVariants()
        {
            await _fixture.ResetDatabase();

            using var context = _fixture.CreateDbContext();
            var service = new ProductService(context);

            var created = await service.CreateProductAsync(new CreateProductDto
            {
                Name = "Laptop",
                Description = "Gaming Laptop",
                CategoryIds = new(),
                OptionIds = new(),
                ProductVariants = new List<CreateProductVariantDto>
                {
                    new() { Sku = "LAP-1", Price = 1000, StockQuantity = 5, OptionValueIds = new() },
                    new() { Sku = "LAP-2", Price = 1200, StockQuantity = 3, OptionValueIds = new() }
                }
            });

            var product = await service.GetProductByIdAsync(created.Id);

            await service.UpdateProductAsync(created.Id, new UpdateProductDto
            {
                Name = "Laptop",
                Description = "Gaming Laptop",
                CategoryIds = new(),
                OptionIds = new(),
                ProductVariants = new List<UpdateProductVariantDto>
                {
                    new()
                    {
                        Id = product!.ProductVariants.First().Id,
                        Sku = "LAP-1",
                        Price = 1000,
                        StockQuantity = 5,
                        OptionValueIds = new()
                    }
                }
            });

            var updated = await service.GetProductByIdAsync(created.Id);

            updated!.ProductVariants.Should().HaveCount(1);
        }

        [Fact]
        public async Task UpdateProduct_ShouldUpdateExistingVariantFields()
        {
            await _fixture.ResetDatabase();

            using var context = _fixture.CreateDbContext();
            var service = new ProductService(context);

            var created = await service.CreateProductAsync(new CreateProductDto
            {
                Name = "Tablet",
                Description = "iPad",
                CategoryIds = new(),
                OptionIds = new(),
                ProductVariants = new List<CreateProductVariantDto>
                {
                    new()
                    {
                        Sku = "TAB-1",
                        Price = 500,
                        StockQuantity = 10,
                        OptionValueIds = new()
                    }
                }
            });

            var product = await service.GetProductByIdAsync(created.Id);
            var variant = product!.ProductVariants.First();

            await service.UpdateProductAsync(created.Id, new UpdateProductDto
            {
                Name = "Tablet",
                Description = "iPad",
                CategoryIds = new(),
                OptionIds = new(),
                ProductVariants = new List<UpdateProductVariantDto>
                {
                    new()
                    {
                        Id = variant.Id,
                        Sku = "TAB-UPDATED",
                        Price = 650,
                        StockQuantity = 7,
                        OptionValueIds = new()
                    }
                }
            });

            var updated = await service.GetProductByIdAsync(created.Id);

            var updatedVariant = updated!.ProductVariants.First();

            updatedVariant.Sku.Should().Be("TAB-UPDATED");
            updatedVariant.Price.Should().Be(650);
            updatedVariant.StockQuantity.Should().Be(7);
        }
    }
}
