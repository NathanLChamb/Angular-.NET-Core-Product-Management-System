using eCommerce.Application.Features.Products.Images.DTOs;
using MediatR;

namespace eCommerce.Application.Features.Products.Images.Commands.UpdateProductOptionValueImage
{
    public record UpdateProductOptionValueImageCommand(
        int ProductId,
        int ImageId,
        string Url,
        bool IsDefault,
        List<int> OptionValueIds
    ) : IRequest<ProductOptionValueImageDto>;
}
