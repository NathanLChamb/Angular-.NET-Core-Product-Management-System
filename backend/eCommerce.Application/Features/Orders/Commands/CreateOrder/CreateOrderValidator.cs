using FluentValidation;

namespace eCommerce.Application.Features.Orders.Commands.CreateOrder
{
    public class CreateOrderValidator : AbstractValidator<CreateOrderCommand>
    {
        public CreateOrderValidator()
        {
            RuleFor(x => x.UserId)
                .NotEmpty();

            RuleFor(x => x.ShippingAddress)
                .NotEmpty()
                .MaximumLength(500);
        }
    }
}
