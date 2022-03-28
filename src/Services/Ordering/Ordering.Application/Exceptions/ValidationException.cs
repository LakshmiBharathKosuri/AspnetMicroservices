using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Application.Exceptions
{
    /*ValidationException is used to throw and exception if we get any validation error 
      from fluent validation rules from 'CheckoutOrderCommandValidator' &
      'UpdateOrderCommandValidator'. 
     
      ApplicationException is from the system library. We use this ApplicationException
     as base which accepts a string
   */
    public class ValidationException: ApplicationException
    {
        public ValidationException()
             : base("One or more validation failures have occurred.")
        {
            Errors = new Dictionary<string, string[]>();
        }


        /*ValidationFailure is from 'using FluentValidation.Results'. if any validation occure 
         during the request in 'CheckoutOrderCommandValidator' & 'UpdateOrderCommandValidator'
         then we use the below method to process the occured validations into dictionary
        */
        public ValidationException(IEnumerable<ValidationFailure> failures)
           : this()
        {
            Errors = failures
                .GroupBy(e => e.PropertyName, e => e.ErrorMessage)
                .ToDictionary(failureGroup => failureGroup.Key, failureGroup => failureGroup.ToArray());
        }
        public IDictionary<string, string[]> Errors { get; }

    }
}
