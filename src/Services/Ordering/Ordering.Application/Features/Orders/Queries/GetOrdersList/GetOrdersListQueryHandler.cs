using AutoMapper;
using MediatR;
using Ordering.Application.Contracts.Persistance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Ordering.Application.Features.Orders.Queries.GetOrdersList
{
    /*IRequestHandler is from MediatR.
      which expects 2 params -> Request and Response
      GetOrdersListQuery is the request param to IRequestHandler & List<OrdersVm>
      is the response param to IRequestHandler
      
      Right click/point on IRequestHandler to implement for the Handle method.
      
      When request comes to IRequest then IRequestHandler will handle the request. To implement the
      request we inject the IRepository & IMapper in this RequestHandler class.
     
     */


    public class GetOrdersListQueryHandler : IRequestHandler<GetOrdersListQuery, List<OrdersVm>>
    {
        //IOrderRepository from the contracts/persistance folder
        private readonly IOrderRepository _orderRepository;

        //using automapper
        private readonly IMapper _mapper;

        //constructor
        public GetOrdersListQueryHandler(IOrderRepository orderRepository, IMapper mapper)
        {
            _orderRepository = orderRepository ?? throw new ArgumentNullException(nameof(orderRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<List<OrdersVm>> Handle(GetOrdersListQuery request, CancellationToken cancellationToken)
        {
            //_orderRepository consist of the method GetOrdersByUserName.

            /*if you notice that, we have used the handler to implement the IOrderRepository method.
              we do not have any db interaction for this method implementation.
              By this way, we have removed the dependency on DB layer or infrastrucre layer from the 
              domain and application layer.
             
              Domain and Application layer combinedly called as Core Layer. We have isolated the core
              layer from the DB layer. So our Core layer [domain + application] do not have any
              dependency on the data base layer and it does not have any knowledge about the which
              DB it interact with.
             
              So, we can easily plug in and plug out the Db layer with the core layer.
             */
            var orderList = await _orderRepository.GetOrdersByUserName(request.UserName);
            return _mapper.Map<List<OrdersVm>>(orderList);
        }
    }
}
