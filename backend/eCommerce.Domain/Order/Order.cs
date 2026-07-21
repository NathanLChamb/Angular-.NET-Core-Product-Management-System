namespace eCommerce.Domain.Order
{
    public class Order
    {
        public int Id { get; set; }
        public decimal TotalPrice { get; set; }
        public required string ShippingAddress { get; set; }
        public required string OrderEmail { get; set; }
        public DateTime OrderDate { get; set; }
        public required string OrderStatus { get; set; }
    }
}
