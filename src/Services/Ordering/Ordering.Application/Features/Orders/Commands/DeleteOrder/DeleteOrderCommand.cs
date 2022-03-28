using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Application.Features.Orders.Commands.DeleteOrder
{
    /*IRequest will comes from the MediatR package. We are trying use the CQRS pattern 
    using the Mediator pattern. Mediator pattern will be available from the MediatR
    nuget package.

    IRequest do not have the response Param in delete business usecase.

    UpdateOrderCommand class has the below properties to update the data into DB based on the Id.
  */
    public class DeleteOrderCommand:IRequest
    {
        public int Id { get; set; }
    }
}
