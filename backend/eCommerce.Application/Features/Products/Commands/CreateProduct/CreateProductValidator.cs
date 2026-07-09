using FluentValidation;

namespace eCommerce.Application.Features.Products.Commands.CreateProduct
{
    public class CreateProductValidator : AbstractValidator<CreateProductCommand>
    {
        public CreateProductValidator()
        {
            RuleFor(p => p.Name)
                .NotEmpty()
                .MaximumLength(50);

            RuleFor(p => p.Description)
                .NotEmpty()
                .MaximumLength(300);

            RuleFor(p => p.CategoryIds)
                .NotEmpty();

            RuleFor(p => p.OptionIds)
                .NotEmpty();

            RuleFor(p => p.ProductVariants)
                .NotEmpty();
        }
    }
}
