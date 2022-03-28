using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Application.Features.Orders.Commands.CheckoutOrder
{
    /*IRequest will comes from the MediatR package. We are trying use the CQRS pattern 
      using the Mediator pattern. Mediator pattern will be available from the MediatR
      nuget package.

      IRequest will have the response Param, here the response is 'int'. As after checkout [insert]
      the order in DB we will get the integer as output.[newly created order ID as an output]

      CheckoutOrderCommand class has the below properties to insert the data into DB.
    */
    public class CheckoutOrderCommand: IRequest<int>
    {
        public string UserName { get; set; }
        public decimal TotalPrice { get; set; }

        // BillingAddress
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public string AddressLine { get; set; }
        public string Country { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }

        // Payment
        public string CardName { get; set; }
        public string CardNumber { get; set; }
        public string Expiration { get; set; }
        public string CVV { get; set; }
        public int PaymentMethod { get; set; }
    }
}
