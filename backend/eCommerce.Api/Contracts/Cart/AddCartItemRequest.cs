namespace eCommerce.Api.Contracts.Cart
{
    public record AddCartItemRequest(
        int ProductVariantId,
        int Quantity
    );
}
