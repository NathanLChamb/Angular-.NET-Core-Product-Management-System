using eCommerce.Domain.Order;

namespace eCommerce.API.Contracts.Orders;

public class UpdateOrderStatusRequest
{
    public OrderStatus Status { get; set; }
}
