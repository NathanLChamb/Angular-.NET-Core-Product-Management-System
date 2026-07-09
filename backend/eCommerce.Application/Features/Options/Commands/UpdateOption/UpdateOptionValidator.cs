using FluentValidation;

namespace eCommerce.Application.Features.Options.Commands.UpdateOption
{
    public class UpdateOptionValidator : AbstractValidator<UpdateOptionCommand>
    {
        public UpdateOptionValidator()
        {
            RuleFor(o => o.Name)
                .NotEmpty()
                .MaximumLength(50);

            RuleFor(o => o.OptionValues)
                .NotEmpty();

            RuleForEach(o => o.OptionValues)
                .ChildRules(value =>
                {
                    value.RuleFor(v => v.Value)
                        .NotEmpty()
                        .MaximumLength(50);
                });
        }
    }
}
