using eCommerce.Application.Features.Orders.DTOs;
using eCommerce.Domain.Order;

namespace eCommerce.Application.Features.Orders.Mappings
{
    public static class OrderProjection
    {
        public static IQueryable<ReadOrderDto> ToOrderDto(this IQueryable<Order> query)
        {
            return query.Select(o => new ReadOrderDto
            {
                Id = o.Id,
                OrderNumber = o.OrderNumber,
                ShippingAddress = o.ShippingAddress,
                TotalPrice = o.TotalPrice,
                Status = o.Status.ToString(),
                OrderDate = o.OrderDate,

                Items = o.OrderItems.Select(i => new ReadOrderItemDto
                {
                    Id = i.Id,
                    ProductName = i.ProductName,
                    Sku = i.Sku,
                    PriceAtPurchase = i.PriceAtPurchase,
                    Quantity = i.Quantity,
                    TotalPrice = i.PriceAtPurchase * i.Quantity
                }).ToList()
            });
        }
    }
}
