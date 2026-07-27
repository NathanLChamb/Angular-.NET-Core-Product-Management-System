namespace eCommerce.Domain.Order
{
    public class Order
    {
        public int Id { get; set; }
        public required string OrderNumber { get; set; }
        public required string UserId { get; set; }
        public decimal TotalPrice { get; set; }
        public required string ShippingAddress { get; set; }
        public DateTime OrderDate { get; set; } = DateTime.UtcNow;
        public OrderStatus Status { get; set; } = OrderStatus.Pending;
        public List<OrderItem> OrderItems { get; set; } = new();
    }
}
