using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Application.Features.Orders.Queries.GetOrdersList
{
    /*IRequest will comes from the MediatR package. We are trying use the CQRS pattern 
      using the Mediator pattern. Mediator pattern will be available from the MediatR
      nuget package.
    
    OrdersVm is a DTO object [c#], which is used to convert the entity object into our C# model
    object.

    IRequest will have the response Param, here the response is List<OrdersVm>
    */
    public class GetOrdersListQuery: IRequest<List<OrdersVm>>
    {
        public string UserName { get; set; }

        public GetOrdersListQuery(string userName)
        {
            UserName = userName ?? throw new ArgumentNullException(nameof(userName));
        }
    }
}
