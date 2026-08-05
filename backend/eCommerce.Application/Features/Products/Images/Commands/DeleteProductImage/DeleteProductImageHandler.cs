using eCommerce.Application.Exceptions;
using eCommerce.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace eCommerce.Application.Features.Products.Images.Commands.DeleteProductImage;

public class DeleteProductImageHandler
    : IRequestHandler<DeleteProductImageCommand>
{
    private readonly IeCommerceContext _context;
    public DeleteProductImageHandler(IeCommerceContext context)
    {
        _context = context;
    }

    public async Task Handle(DeleteProductImageCommand request, CancellationToken ct)
    {
        var image = await _context.ProductImages
            .FirstOrDefaultAsync(pi => pi.Id == request.ImageId && pi.ProductId == request.ProductId, ct);
        if (image == null) throw new NotFoundException("Product image not found.");
        
        _context.ProductImages.Remove(image);
        await _context.SaveChangesAsync(ct);
    }
}