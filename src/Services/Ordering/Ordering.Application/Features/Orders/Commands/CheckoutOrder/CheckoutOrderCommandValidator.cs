using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Application.Features.Orders.Commands.CheckoutOrder
{
    public class CheckoutOrderCommandValidator : AbstractValidator<CheckoutOrderCommand>
    {
        public CheckoutOrderCommandValidator()
        {
            RuleFor(p => p.UserName)
                .NotEmpty().WithMessage("{UserName} is requried")
                .NotNull()
                .MaximumLength(50).WithMessage("{UserName} must not excedd 50 chars");

            RuleFor(p => p.EmailAddress)
                  .NotEmpty().WithMessage("{EmailAdress} is requried");

            RuleFor(p => p.TotalPrice)
                .NotEmpty().WithMessage("{TotalPrice} is requried")
                .GreaterThan(0).WithMessage("{TotalPrice} must be greater than zero");


        }
    }
}
