using eCommerce.Application.Features.Products.Filters;
using eCommerce.Application.Features.Products.Queries.GetAllProducts;
using eCommerce.Domain.Product;
using eCommerce.Infrastructure.Persistence;
using eCommerce.Tests.Infrastructure;
using FluentAssertions;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace eCommerce.Tests.Application
{
    [Collection("Database Collection")]
    public class ProductQueryTests
    {
        private readonly PostgresContainerFixture _fixture;
        public ProductQueryTests(PostgresContainerFixture fixture)
        {
            _fixture = fixture;
        }
        [Fact]
        public async Task GetAllProducts_WithNoFilters_ShouldReturnPagedProducts()
        {
            await _fixture.ResetDatabase();

            using var scope = _fixture.Services.CreateScope();
            var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
            var context = scope.ServiceProvider.GetRequiredService<eCommerceContext>();

            context.Products.AddRange(
                new Product
                {
                    Name = "Phone",
                    Description = "Mobile"
                },
                new Product
                {
                    Name = "Laptop",
                    Description = "Computer"
                }
            );

            await context.SaveChangesAsync();

            var result = await mediator.Send(new GetAllProductsQuery(
                new ProductSearchFilter
                {
                    PageNumber = 1,
                    PageSize = 10
                })
            );

            result.Items.Should().HaveCount(2);
        }

        [Fact]
        public async Task GetAllProducts_WithSearch_ShouldReturnMatchingProducts()
        {

        }

        [Fact]
        public async Task GetAllProducts_WithMultipleCategories_ShouldUseOrLogic()
        {

        }

        [Fact]
        public async Task GetAllProducts_WithMultipleOptions_ShouldUseAndLogic()
        {

        }

        [Fact]
        public async Task GetAllProducts_WithPriceAscending_ShouldSortCorrectly()
        {

        }

        [Fact]
        public async Task GetAllProducts_WithNewestSort_ShouldReturnLatestFirst()
        {

        }
    }
}
