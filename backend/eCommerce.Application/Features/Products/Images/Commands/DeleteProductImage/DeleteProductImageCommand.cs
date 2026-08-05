using MediatR;

namespace eCommerce.Application.Features.Products.Images.Commands.DeleteProductImage;

public record DeleteProductImageCommand(
    int ProductId,
    int ImageId
) : IRequest;
