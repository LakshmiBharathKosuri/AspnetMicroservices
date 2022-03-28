using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Application.Features.Orders.Commands.UpdateOrder
{
    /*AbstractValidator is from the FluentValidation.
      class UpdateOrderCommandValidator is used to validate the request before the
      execution of IRequestHandler.

      It mean, the request first validate and then it goes to IRequestHandler from there
      it will go to Db/ infrastructure layer.

      AbstractValidator requires an argument, to which its going to validate.
      Now its going to validate the "UpdateOrderCommand" request.
      */
    public class UpdateOrderCommandValidator: AbstractValidator<UpdateOrderCommand>
    {
        //Inside the constructor, we place the fluent validation rules for the request.
        public UpdateOrderCommandValidator()
        {
            RuleFor(p => p.UserName)
                .NotEmpty().WithMessage("{UserName} is required.")
                .NotNull()
                .MaximumLength(50).WithMessage("{UserName} must not exceed 50 characters.");

            RuleFor(p => p.EmailAddress)
               .NotEmpty().WithMessage("{EmailAddress} is required.");

            RuleFor(p => p.TotalPrice)
                .NotEmpty().WithMessage("{TotalPrice} is required.")
                .GreaterThan(0).WithMessage("{TotalPrice} should be greater than zero.");
        }
    }
}
