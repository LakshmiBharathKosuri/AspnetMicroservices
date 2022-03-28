using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ValidationException = Ordering.Application.Exceptions.ValidationException;

namespace Ordering.Application.Behaviours
{
    /*Now that we have our validator set up, Let’s add the Validation to the Pipeline Behaviour.
      IPipelineBehavior is from 'using MediatR'. 
      IValidator is from 'using FluentValidation'
     */

    public class ValidationBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        /*
         Here [IEnumerable<IValidator<TRequest>> _validators], we get a list of all registered validators.
         IValidator get all the validations from the assembly using reflection.
         In our case we have validators in 'CheckoutOrderCommandValidator' &
         'UpdateOrderCommandValidator'[ both are inheriting from AbstractValidator]
        */
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        //Initializing the validator, and also validating the request.
        public ValidationBehaviour(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators ?? throw new ArgumentNullException(nameof(validators));
        }

        //Here is the Handler method. we will get the handle method while implementing the
        // IPipelineBehavior.
        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken,
            RequestHandlerDelegate<TResponse> next)
        {
            //If any validation errors were found, It extracts all the messages
            //and returns the errors as a reposnse.
            if (_validators.Any())
            {
                //ValidationContext is from 'using FluentValidation'. We use context object 
                // to get the context from the request.
                var context = new ValidationContext<TRequest>(request);

                var validationResults = await Task.WhenAll(_validators.Select(v => v.ValidateAsync(context, cancellationToken)));
                var failures = validationResults.SelectMany(r => r.Errors).Where(f => f != null).ToList();

                //we are calling our custom 'Ordering.Application.Exceptions.ValidationException'
                if (failures.Count != 0)
                    throw new ValidationException(failures);
            }
            //go for the next validation
            return await next();
        }
    }
}
