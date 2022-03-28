using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBus.Messages.Events
{
    /*
      why the properites in this class are same as order entity?
      Cause, when this event is fired /produced the information is used by the
      consumer i.e orderign API. the motive is the oreding API needs to create an order 
      in DB using this event. So, we copied all the parameters from order entity to 
      BasketCheckoutEvent class/ to this class.
      
      this calss inherit from the IntegrationBaseEvent for the common properties.
     */
    public class BasketCheckoutEvent: IntegrationBaseEvent
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
