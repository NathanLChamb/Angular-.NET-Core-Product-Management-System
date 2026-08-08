using FluentValidation;

namespace eCommerce.Application.Features.Products.Images.Commands.AddProductOptionValueImage
{
    public class AddProductOptionValueImageValidator : AbstractValidator<AddProductOptionValueImageCommand>
    {
        public AddProductOptionValueImageValidator()
        {
            RuleFor(x => x.ProductId)
                .GreaterThan(0);

            RuleFor(x => x.Url)
                .NotEmpty()
                .MaximumLength(500);

            RuleFor(x => x.OptionValueIds)
                .NotEmpty()
                .Must(ids => ids.Distinct().Count() == ids.Count)
                .WithMessage("Option value IDs must be unique.");

            RuleForEach(x => x.OptionValueIds)
                .GreaterThan(0);
        }
    }
}
