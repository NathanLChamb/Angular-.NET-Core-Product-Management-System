using FluentValidation;

namespace eCommerce.Application.Features.Products.Images.Commands.AddProductImage
{
    public class AddProductImageValidator
        : AbstractValidator<AddProductImageCommand>
    {
        public AddProductImageValidator()
        {
            RuleFor(x => x.ProductId)
                .GreaterThan(0);

            RuleFor(x => x.Url)
                .NotEmpty()
                .MaximumLength(500);

            RuleFor(x => x.DisplayOrder)
                .GreaterThanOrEqualTo(0);
        }
    }
}