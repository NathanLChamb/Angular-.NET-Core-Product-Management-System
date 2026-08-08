namespace eCommerce.Application.Features.Orders.Filters
{
    public class OrderSearchFilter
    {
        public OrderStatusFilter Status { get; set; } = OrderStatusFilter.All;
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}
