using FluentValidation;

namespace eCommerce.Application.Features.Products.Images.Commands.UpdateProductOptionValueImage
{
    public class UpdateProductOptionValueImageValidator : AbstractValidator<UpdateProductOptionValueImageCommand>
    {
        public UpdateProductOptionValueImageValidator()
        {
            RuleFor(x => x.OptionValueIds)
                .NotEmpty();

            RuleForEach(x => x.OptionValueIds)
                .GreaterThan(0);

            RuleFor(x => x.Url)
                .NotEmpty()
                .MaximumLength(500);
        }
    }
}
