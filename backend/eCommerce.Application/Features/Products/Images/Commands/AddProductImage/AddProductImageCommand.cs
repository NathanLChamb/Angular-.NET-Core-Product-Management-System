using eCommerce.Application.Features.Products.Images.DTOs;
using MediatR;

namespace eCommerce.Application.Features.Products.Images.Commands.AddProductImage
{
    public record AddProductImageCommand(
        int ProductId,
        string Url,
        int DisplayOrder
    ) : IRequest<ProductImageDto>;
}