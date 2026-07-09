using eCommerce.Application.Exceptions;
using eCommerce.Application.Interfaces;
using MediatR;

namespace eCommerce.Application.Features.Products.Commands.DeleteProduct
{
    public class DeleteProductHandler : IRequestHandler<DeleteProductCommand>
    {
        private readonly IeCommerceContext _context;
        public DeleteProductHandler(IeCommerceContext context)
        {
            _context = context;
        }

        public async Task Handle(DeleteProductCommand request, CancellationToken ct)
        {
            var product = await _context.Products.FindAsync(request.Id);
            if (product == null) throw new NotFoundException("Product for deletion not found from provided ID");

            _context.Products.Remove(product);
            await _context.SaveChangesAsync(ct);
        }
    }
}
