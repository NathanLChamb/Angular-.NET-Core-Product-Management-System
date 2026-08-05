using eCommerce.Application.Features.Orders.Queries.GetMyOrders;
using eCommerce.Domain.Order;
using eCommerce.Infrastructure.Persistence;
using eCommerce.Tests.Infrastructure;
using FluentAssertions;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace eCommerce.Tests.Application.Orders
{
    [Collection("Database Collection")]
    public class OrderQueryTests
    {
        private readonly PostgresContainerFixture _fixture;

        public OrderQueryTests(PostgresContainerFixture fixture)
        {
            _fixture = fixture;
        }


        [Fact]
        public async Task GetMyOrders_ShouldReturnOnlyUsersOrders()
        {
            await _fixture.ResetDatabase();

            using var scope = _fixture.Services.CreateScope();

            var mediator =
                scope.ServiceProvider.GetRequiredService<IMediator>();

            var context =
                scope.ServiceProvider.GetRequiredService<eCommerceContext>();


            context.Orders.AddRange(
                new Order
                {
                    UserId = "user-1",
                    OrderNumber = "ORD-1",
                    ShippingAddress = "Address 1",
                    TotalPrice = 100,
                    OrderDate = DateTime.UtcNow,
                    Status = OrderStatus.Pending
                },
                new Order
                {
                    UserId = "user-2",
                    OrderNumber = "ORD-2",
                    ShippingAddress = "Address 2",
                    TotalPrice = 200,
                    OrderDate = DateTime.UtcNow,
                    Status = OrderStatus.Pending
                }
            );

            await context.SaveChangesAsync();


            var result = await mediator.Send(
                new GetMyOrdersQuery("user-1"));


            result.Should().HaveCount(1);
            result[0].OrderNumber.Should().Be("ORD-1");
        }
    }
}