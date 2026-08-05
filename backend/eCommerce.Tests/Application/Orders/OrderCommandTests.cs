using eCommerce.Application.Features.Orders.Commands.CreateOrder;
using eCommerce.Domain.Cart;
using eCommerce.Domain.Product;
using eCommerce.Infrastructure.Persistence;
using eCommerce.Tests.Infrastructure;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;

namespace eCommerce.Tests.Application.Orders
{
    [Collection("Database Collection")]
    public class OrderCommandTests
    {
        private readonly PostgresContainerFixture _fixture;

        public OrderCommandTests(PostgresContainerFixture fixture)
        {
            _fixture = fixture;
        }
        [Fact]
        public async Task CreateOrder_WithValidCart_ShouldCreateOrderAndClearCart()
        {
            await _fixture.ResetDatabase();

            using var scope = _fixture.Services.CreateScope();
            var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
            var context = scope.ServiceProvider.GetRequiredService<eCommerceContext>();

            var product = new Product
            {
                Name = "Phone",
                Description = "Test phone"
            };

            var variant = new ProductVariant
            {
                Product = product,
                Sku = "PHONE-1",
                Price = 500,
                StockQuantity = 10
            };

            context.Products.Add(product);
            context.ProductVariants.Add(variant);

            await context.SaveChangesAsync();


            context.Carts.Add(new Domain.Cart.Cart
            {
                UserId = "user-1",
                Items =
        {
            new CartItem
            {
                ProductVariantId = variant.Id,
                ProductVariant = variant,
                Quantity = 2
            }
        }
            });

            await context.SaveChangesAsync();


            var result = await mediator.Send(
                new CreateOrderCommand(
                    "user-1",
                    "123 Test Street")
            );


            result.Should().NotBeNull();
            result.Items.Should().HaveCount(1);
            result.TotalPrice.Should().Be(1000);


            var cart = await context.Carts
                .Include(c => c.Items)
                .FirstAsync();

            cart.Items.Should().BeEmpty();


            var updatedVariant =
                await context.ProductVariants
                    .FirstAsync();

            updatedVariant.StockQuantity.Should().Be(8);
        }
        [Fact]
        public async Task CancelOrder_ShouldRestoreStock()
        {

        }
        [Fact]
        public async Task CancelOrder_WhenAlreadyShipped_ShouldFail()
        {

        }
        [Fact]
        public async Task UpdateOrderStatus_ShouldChangeStatus()
        {

        }
        [Fact]
        public async Task UpdateDeliveredOrder_ShouldFail()
        {

        }
        [Fact]
        public async Task UserCannotAccessOtherUsersOrder()
        {

        }
    }
}
