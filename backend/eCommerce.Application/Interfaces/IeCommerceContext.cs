using eCommerce.Domain.Cart;
using eCommerce.Domain.Metadata;
using eCommerce.Domain.Order;
using eCommerce.Domain.Product;
using Microsoft.EntityFrameworkCore;

namespace eCommerce.Application.Interfaces
{
    public interface IeCommerceContext
    {
        DbSet<Product> Products { get; }
        DbSet<Category> Categories { get; }
        DbSet<Option> Options { get; }
        DbSet<ProductVariant> ProductVariants { get; }
        DbSet<OptionValue> OptionValues { get; }
        DbSet<Cart> Carts { get; }
        DbSet<CartItem> CartItems { get; }
        DbSet<Order> Orders { get; }
        DbSet<OrderItem> OrderItems { get; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
