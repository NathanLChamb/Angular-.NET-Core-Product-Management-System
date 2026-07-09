using FluentValidation;

namespace eCommerce.Application.Features.Options.Commands.CreateOption
{
    public class CreateOptionValidator : AbstractValidator<CreateOptionCommand>
    {
        public CreateOptionValidator()
        {
            RuleFor(o => o.Name)
                .NotEmpty()
                .MaximumLength(50);

            RuleFor(o => o.OptionValues)
                .NotEmpty();

            RuleForEach(o => o.OptionValues)
                .MaximumLength(50);
        }
    }
}
