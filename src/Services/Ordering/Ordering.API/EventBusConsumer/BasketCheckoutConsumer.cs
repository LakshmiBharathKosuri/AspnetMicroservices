using AutoMapper;
using EventBus.Messages.Events;
using MassTransit;
using MediatR;
using Microsoft.Extensions.Logging;
using Ordering.Application.Features.Orders.Commands.CheckoutOrder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ordering.API.EventBusConsumer
{
    /*
     BasketCheckoutConsumer should inherit IConsumer.
     IConsumer is from MassTransit.
     
     IConsumer take the event parameter BasketCheckoutEvent.
     Upon implementing the ICosumer, we will have a consume method.

     We know that, to create a order our ordering.api is using the CQRS pattern.
     So the implementation of Consume method also inline with the CQRS pattern.
     
     Ordring API -> recieves the request in command-> invokes the handler -> & then
     calls the repo to create the order. So, on the same lines our consume method 
     should work.

     */
    public class BasketCheckoutConsumer : IConsumer<BasketCheckoutEvent>
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        private readonly ILogger<BasketCheckoutConsumer> _logger;

        public BasketCheckoutConsumer(IMediator mediator, IMapper mapper, ILogger<BasketCheckoutConsumer> logger)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task Consume(ConsumeContext<BasketCheckoutEvent> context)
        {
            //create a mapping b/w the CheckoutOrderCommand and BasketCheckoutEvent
            // as for creating order MediatR handler expecting the CheckoutOrderCommand

            //context.Message contains the BasketCheckoutEvent, which is maps to CheckoutOrderCommand
            var command = _mapper.Map<CheckoutOrderCommand>(context.Message);

            //CheckoutOrderCommand handler recieves the command request, from where
            // it calls the repository to create the order in DB.
            var result = await _mediator.Send(command);

            _logger.LogInformation("BasketCheckoutEvent consumed successfully. Created Order Id : {newOrderId}", result);
        }
    }
}
