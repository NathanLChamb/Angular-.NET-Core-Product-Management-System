using MediatR;

namespace eCommerce.Application.Features.Products.Images.Commands.DeleteProductOptionValueImage
{
    public record DeleteProductOptionValueImageCommand(
        int ProductId,
        int ImageId
    ) : IRequest;
}
