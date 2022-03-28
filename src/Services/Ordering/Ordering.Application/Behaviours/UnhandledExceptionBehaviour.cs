using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Ordering.Application.Behaviours
{
    /* UnhandledExceptionBehaviour is used to catch the unhandled exceptions.
       In our business logic we have not used the try- catch blocks to catch the exceptions.
       So to make our code cleaner, we created a UnhandledExceptionBehaviour and added it to
       the IPipelineBehavior. So If any exception comes from handler then the method 'Handle'
       will take care of it.

       IPipelineBehavior is from 'using MediatR'. 
       IValidator is from 'using FluentValidation'
    */
    public class UnhandledExceptionBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest:IRequest<TResponse>
    {
        //we will log the UnhandledException using logger.
        private readonly ILogger<TRequest> _logger;
        public UnhandledExceptionBehaviour(ILogger<TRequest> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            try
            {
                // continue the pipe line to check any/all the exceptions.  
                return await next();
            }
            catch (Exception ex)
            {
                //if an exception found then it will log the exceptions from catch block
                var requestName = typeof(TRequest).Name;
                _logger.LogError(ex, "Application Request: Unhandled Exception for Request {Name} {@Request}", requestName, request);
                throw;
            }
        }
    }
}
