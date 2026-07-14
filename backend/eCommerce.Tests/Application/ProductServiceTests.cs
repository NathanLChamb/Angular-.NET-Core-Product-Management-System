using eCommerce.Application.Features.Products.Commands.CreateProduct;
using eCommerce.Application.Features.Products.Commands.UpdateProduct;
using eCommerce.Application.Features.Products.DTOs;
using eCommerce.Application.Features.Products.Queries.GetProductById;
using eCommerce.Infrastructure.Persistence;
using eCommerce.Tests.Infrastructure;
using FluentAssertions;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

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

            using var scope = _fixture.Services.CreateScope();
            var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
            var context = scope.ServiceProvider.GetRequiredService<eCommerceContext>();

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

            var result = await mediator.Send(new CreateProductCommand(dto.Name, dto.Description, dto.CategoryIds, dto.OptionIds, dto.ProductVariants));

            result.Should().NotBeNull();
            result.Id.Should().BeGreaterThan(0);
            result.ProductVariants.Should().HaveCount(1);
        }

        [Fact]
        public async Task UpdateProduct_ShouldRemoveDeletedVariants()
        {
            await _fixture.ResetDatabase();

            using var scope = _fixture.Services.CreateScope();
            var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
            var context = scope.ServiceProvider.GetRequiredService<eCommerceContext>();

            var created = await mediator.Send(new CreateProductCommand(
                "Laptop",
                "Gaming Laptop",
                new(),
                new(),
                new List<CreateProductVariantDto>
                {
                    new() { Sku = "LAP-1", Price = 1000, StockQuantity = 5, OptionValueIds = new() },
                    new() { Sku = "LAP-2", Price = 1200, StockQuantity = 3, OptionValueIds = new() }
                })
            );

            var product = await mediator.Send(new GetProductByIdQuery(created.Id));

            await mediator.Send(new UpdateProductCommand(
                created.Id,
                "Laptop",
                "Gaming Laptop",
                new(),
                new(),
                new List<UpdateProductVariantDto>
                {
                    new()
                    {
                        Id = product!.ProductVariants.First().Id,
                        Sku = "LAP-1",
                        Price = 1000,
                        StockQuantity = 5,
                        OptionValueIds = new()
                    }
                })
            );

            var updated = await mediator.Send(new GetProductByIdQuery(created.Id));

            updated!.ProductVariants.Should().HaveCount(1);
        }

        [Fact]
        public async Task UpdateProduct_ShouldUpdateExistingVariantFields()
        {
            await _fixture.ResetDatabase();

            using var scope = _fixture.Services.CreateScope();
            var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
            var context = scope.ServiceProvider.GetRequiredService<eCommerceContext>();

            var created = await mediator.Send(new CreateProductCommand(
                "Tablet",
                "iPad",
                new(),
                new(),
                new List<CreateProductVariantDto>
                {
                    new()
                    {
                        Sku = "TAB-1",
                        Price = 500,
                        StockQuantity = 10,
                        OptionValueIds = new()
                    }
                })
            );

            var product = await mediator.Send(new GetProductByIdQuery(created.Id));
            var variant = product!.ProductVariants.First();

            await mediator.Send(new UpdateProductCommand(
                created.Id,
                "Tablet",
                "iPad",
                new(),
                new(),
                new List<UpdateProductVariantDto>
                {
                    new()
                    {
                        Id = variant.Id,
                        Sku = "TAB-UPDATED",
                        Price = 650,
                        StockQuantity = 7,
                        OptionValueIds = new()
                    }
                })
            );

            var updated = await mediator.Send(new GetProductByIdQuery(created.Id));

            var updatedVariant = updated!.ProductVariants.First();

            updatedVariant.Sku.Should().Be("TAB-UPDATED");
            updatedVariant.Price.Should().Be(650);
            updatedVariant.StockQuantity.Should().Be(7);
        }

        [Fact]
        public async Task CreateProduct_WithInvalidCategory_ShouldFail()
        {
        }

        [Fact]
        public async Task CreateProduct_WithInvalidOptionValue_ShouldFail()
        {

        }

        [Fact]
        public async Task CreateProduct_WithDuplicateSku_ShouldFail()
        {

        }

        [Fact]
        public async Task UpdateProduct_WithInvalidVariant_ShouldFail()
        {

        }
    }
}
