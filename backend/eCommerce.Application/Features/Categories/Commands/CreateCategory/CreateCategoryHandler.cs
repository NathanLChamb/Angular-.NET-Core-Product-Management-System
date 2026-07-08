using eCommerce.Application.Features.Categories.DTOs;
using eCommerce.Application.Interfaces;
using eCommerce.Domain.Metadata;
using MediatR;

namespace eCommerce.Application.Features.Categories.Commands.CreateCategory
{
    public class CreateCategoryHandler : IRequestHandler<CreateCategoryCommand, ReadCategoryDto>
    {
        private readonly IeCommerceContext _context;
        public CreateCategoryHandler(IeCommerceContext context)
        {
            _context = context;
        }

        public async Task<ReadCategoryDto> Handle(CreateCategoryCommand request, CancellationToken ct)
        {
            var newCategory = new Category
            {
                Name = request.Name,
                Description = request.Description
            };

            _context.Categories.Add(newCategory);
            await _context.SaveChangesAsync(ct);

            return new ReadCategoryDto
            {
                Id = newCategory.Id,
                Name = newCategory.Name,
                Description = newCategory.Description
            };
        }
    }
}
