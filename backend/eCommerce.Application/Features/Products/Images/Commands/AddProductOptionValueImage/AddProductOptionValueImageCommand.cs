using eCommerce.Application.Features.Products.Images.DTOs;
using MediatR;

namespace eCommerce.Application.Features.Products.Images.Commands.AddProductOptionValueImage
{
    public record AddProductOptionValueImageCommand(
        int ProductId,
        string Url,
        bool IsDefault,
        List<int> OptionValueIds
    ) : IRequest<ProductOptionValueImageDto>;
}
