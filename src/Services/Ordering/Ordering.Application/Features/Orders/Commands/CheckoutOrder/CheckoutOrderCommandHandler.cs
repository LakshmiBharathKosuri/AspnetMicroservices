using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Ordering.Application.Contracts.Infrastructure;
using Ordering.Application.Contracts.Persistance;
using Ordering.Application.Models;
using Ordering.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Ordering.Application.Features.Orders.Commands.CheckoutOrder
{
    /*IRequestHandler is from MediatR.
     which expects 2 params -> Request and Response
     CheckoutOrderCommand is the request param to IRequestHandler & 'int'
     is the response param to IRequestHandler

     Right click/point on IRequestHandler to implement for the Handle method.

     When request comes to IRequest then IRequestHandler will handle the request. To implement the
     request we inject the IRepository & IMapper in this RequestHandler class.

    */
    public class CheckoutOrderCommandHandler : IRequestHandler<CheckoutOrderCommand, int>
    {
        //IOrderRepository is from Ordering.Application.Contracts.Persistence
        private readonly IOrderRepository _orderRepository;

        //IMapper is from Automapper
        private readonly IMapper _mapper;

        //IEmailService is from Ordering.Application.Contracts.Infrastructure
        private readonly IEmailService _emailService;

        //ILogger is from using Microsoft.Extensions.Logging;
        private ILogger<CheckoutOrderCommandHandler> _logger;

        //constructor
        public CheckoutOrderCommandHandler(IOrderRepository orderRepository, IMapper mapper, IEmailService emailService, ILogger<CheckoutOrderCommandHandler> logger)
        {
            _orderRepository = orderRepository ?? throw new ArgumentNullException(nameof(orderRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _emailService = emailService ?? throw new ArgumentNullException(nameof(emailService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<int> Handle(CheckoutOrderCommand request, CancellationToken cancellationToken)
        {
            //before saving we need to convert the request DTO object into the underlying DB entity.
            var orderEntity = _mapper.Map<Order>(request);
            var newOrder = await _orderRepository.AddAsync(orderEntity);

            _logger.LogInformation($"Order {newOrder.Id} is successfully created");
            await SendEmail(newOrder);

            return newOrder.Id;
        }

        private async Task SendEmail(Order order)
        {
            // Email is from Ordering.Application.Models
            var email = new Email { To = "bharath.kosuri@qentelli.com", Body = $"Order was created", Subject = "Order was created" };

            try
            {
                //implement the SendEmail in the request handler
                await _emailService.SendEmail(email);
            }
            catch(Exception ex)
            {
                _logger.LogInformation($"Order with {order.Id} is failed due to an error with email service : {ex.Message}");
            }
        }
    }
}
