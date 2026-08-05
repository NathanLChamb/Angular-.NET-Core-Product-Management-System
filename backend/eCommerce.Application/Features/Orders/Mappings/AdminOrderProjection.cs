using eCommerce.Domain.Order;

namespace eCommerce.Application.Features.Orders.Mappings
{
    public static class AdminOrderProjection
    {
        public static IQueryable<ReadOrderFromAdminDto> ToAdminOrderDto(this IQueryable<Order> query)
        {
            return query.Select(o => new ReadOrderFromAdminDto
            {
                Id = o.Id,
                UserId = o.UserId,
                OrderNumber = o.OrderNumber,
                TotalPrice = o.TotalPrice,
                ShippingAddress = o.ShippingAddress,
                Status = o.Status.ToString(),
                OrderDate = o.OrderDate
            });
        }
    }
}
