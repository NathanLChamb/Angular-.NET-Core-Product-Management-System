using System.ComponentModel.DataAnnotations;

namespace eCommerce.API.Contracts.Orders;

public class CreateOrderRequest
{
    [Required]
    [MaxLength(500)]
    public string ShippingAddress { get; set; } = string.Empty;
}
