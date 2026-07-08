using eCommerce.Application.Exceptions;
using eCommerce.Application.Interfaces;
using MediatR;

namespace eCommerce.Application.Features.Categories.Commands.DeleteCategory
{
    public class DeleteCategoryHandler : IRequestHandler<DeleteCategoryCommand>
    {
        private readonly IeCommerceContext _context;
        public DeleteCategoryHandler(IeCommerceContext context)
        {
            _context = context;
        }

        public async Task Handle(DeleteCategoryCommand request, CancellationToken ct)
        {
            var category = await _context.Categories.FindAsync(request.Id);
            if (category == null) throw new NotFoundException("Category for deletion not found from provided ID");

            _context.Categories.Remove(category);
            await _context.SaveChangesAsync(ct);
        }
    }
}
