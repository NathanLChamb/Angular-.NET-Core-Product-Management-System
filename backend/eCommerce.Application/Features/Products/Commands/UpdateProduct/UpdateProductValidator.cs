using FluentValidation;

namespace eCommerce.Application.Features.Products.Commands.UpdateProduct
{
    public class UpdateProductValidator : AbstractValidator<UpdateProductCommand>
    {
        public UpdateProductValidator()
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

            RuleForEach(p => p.ProductVariants)
                .ChildRules(variant =>
                {
                    variant.RuleFor(v => v.Sku)
                        .NotEmpty()
                        .MaximumLength(10);

                    variant.RuleFor(v => v.Price)
                        .GreaterThanOrEqualTo(0);

                    variant.RuleFor(v => v.StockQuantity)
                        .GreaterThanOrEqualTo(0);

                    variant.RuleFor(v => v.OptionValueIds)
                        .NotEmpty();
                });
        }
    }
}
