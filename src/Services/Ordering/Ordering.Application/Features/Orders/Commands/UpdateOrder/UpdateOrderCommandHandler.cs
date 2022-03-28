using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Ordering.Application.Contracts.Persistance;
using Ordering.Application.Exceptions;
using Ordering.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Ordering.Application.Features.Orders.Commands.UpdateOrder
{
    public class UpdateOrderCommandHandler : IRequestHandler<UpdateOrderCommand>
    {
        //IOrderRepository is from Ordering.Application.Contracts.Persistence
        private readonly IOrderRepository _orderRepository;

        //IMapper is from Automapper
        private readonly IMapper _mapper;

        //ILogger is from using Microsoft.Extensions.Logging;
        private readonly ILogger<UpdateOrderCommandHandler> _logger;

        public UpdateOrderCommandHandler(IOrderRepository orderRepository, IMapper mapper, ILogger<UpdateOrderCommandHandler> logger)
        {
            _orderRepository = orderRepository ?? throw new ArgumentNullException(nameof(orderRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        // if there is no return type for the MediatR, we will use 'Unit' as the return type
        public async Task<Unit> Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
        {
            //get the order from DB using id
            var orderToUpdate = await _orderRepository.GetByIdAsync(request.Id);

            //if order is null we will throw the custom NotFoundException
            if (orderToUpdate == null)
            {
                throw new NotFoundException(nameof(Order), request.Id);
            }
            //map the request[source] to destination for update the db
            _mapper.Map(request, orderToUpdate, typeof(UpdateOrderCommand), typeof(Order));

            //update the db with enity object
            await _orderRepository.UpdateAsync(orderToUpdate);

            _logger.LogInformation($"Order {orderToUpdate.Id} is successfully updated.");

            // if there is no return type for the MediatR, we will use 'Unit' as the return type
            return Unit.Value;
        }
    }
}
