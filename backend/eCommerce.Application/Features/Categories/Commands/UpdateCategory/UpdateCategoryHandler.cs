using eCommerce.Application.Exceptions;
using eCommerce.Application.Interfaces;
using MediatR;

namespace eCommerce.Application.Features.Categories.Commands.UpdateCategory
{
    public class UpdateCategoryHandler : IRequestHandler<UpdateCategoryCommand>
    {
        private readonly IeCommerceContext _context;
        public UpdateCategoryHandler(IeCommerceContext context)
        {
            _context = context;
        }

        public async Task Handle(UpdateCategoryCommand request, CancellationToken ct)
        {
            var category = await _context.Categories.FindAsync(request.Id, ct);
            if (category == null) throw new NotFoundException("Category for update not found from provided ID");

            category.Name = request.Name ?? category.Name;
            category.Description = request.Description ?? category.Description;

            await _context.SaveChangesAsync(ct);
        }
    }
}
