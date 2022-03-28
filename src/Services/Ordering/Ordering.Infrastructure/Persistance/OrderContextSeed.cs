using Microsoft.Extensions.Logging;
using Ordering.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Infrastructure.Persistance
{
    public class OrderContextSeed
    {
        //create a static method with name SeedAsync
        //parameters : OrderContext orderContext, ILogger<OrderContextSeed> logger
        public static async Task SeedAsync(OrderContext orderContext, ILogger<OrderContextSeed> logger)
        {
            if (!orderContext.Orders.Any())
            {
                orderContext.Orders.AddRange(GetPreconfiguredOrders());
                await orderContext.SaveChangesAsync();
                logger.LogInformation("Seed database associated with context {DbContextName}", typeof(OrderContext).Name);
            }
        }

        private static IEnumerable<Order> GetPreconfiguredOrders()
        {
            //we no need to give the createdDate and CreatedBy here. As we have overrided the
            //method SaveChangesAsync in OrderContext class. when the state changes then the 
            //properties will updated from OrderContext class method.
            return new List<Order>
            {
                new Order() {UserName = "kbk", FirstName = "Bharath", LastName = "Kumar", EmailAddress = "bharath@gmail.com", AddressLine = "Hyderbad", Country = "India", TotalPrice = 350 }
            };
        }
    }
}
