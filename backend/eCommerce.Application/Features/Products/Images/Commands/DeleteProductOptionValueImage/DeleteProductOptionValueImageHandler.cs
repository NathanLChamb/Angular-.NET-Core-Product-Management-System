using eCommerce.Application.Exceptions;
using eCommerce.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace eCommerce.Application.Features.Products.Images.Commands.DeleteProductOptionValueImage
{
    public class DeleteProductOptionValueImageHandler : IRequestHandler<DeleteProductOptionValueImageCommand>
    {
        private readonly IeCommerceContext _context;

        public DeleteProductOptionValueImageHandler(IeCommerceContext context)
        {
            _context = context;
        }

        public async Task Handle(DeleteProductOptionValueImageCommand request, CancellationToken ct)
        {
            var image = await _context.ProductOptionValueImages
                .FirstOrDefaultAsync(image => image.Id == request.ImageId && image.ProductId == request.ProductId, ct);

            if (image == null) throw new NotFoundException("Product option value image not found.");
            
            _context.ProductOptionValueImages.Remove(image);
            await _context.SaveChangesAsync(ct);
        }
    }
}
