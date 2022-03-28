using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Ordering.Application.Behaviours;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Application
{
    /*
     Inorder to use the services, we have to register in the startup.cs. So, instead of 
     registering all the services in startup.cs we will create a separate static class -> inside
     this class we will create separate static method which is of type 'IServiceCollection'. In this
     method , we will register all our services and add this extension method in the startup.cs 
     configure services.

     This way we can keep our code cleaner. ApplicationServiceRegistration class is created
     direclty under the Ordering.Application project.
     
     */
    public static class ApplicationServiceRegistration
    {
        //IServiceCollection is from using Microsoft.Extensions.DependencyInjection.
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            //Assembly uses the 'System.Reflection'

            /*
             AddAutoMapper is not a directly available in IServiceCollection. we need to install
             a package : install-package Automapper.Extensions.Microsoft.DependencyInjection
             which will register the automapper with startup.cs
             */
            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            /*
               AddValidatorsFromAssembly is not a directly available in IServiceCollection. 
               we need to install a package :-
               install-package FluentValidation.DependencyInjectionExtensions
               which will register the all validators [IValidator, AbstractValidator etc..]
               with startup.cs
             */
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

            // AddMediatR is from using MediatR
            services.AddMediatR(Assembly.GetExecutingAssembly());

            //Register the pipe line behaviors 
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(UnhandledExceptionBehaviour<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));

            return services;
        }
    }
}
