using AutoMapper;
using Ordering.Application.Features.Orders.Commands.CheckoutOrder;
using Ordering.Application.Features.Orders.Commands.UpdateOrder;
using Ordering.Application.Features.Orders.Queries.GetOrdersList;
using Ordering.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Application.Mappings
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            //create the map between the entity class and DTO class
            CreateMap<Order, OrdersVm>().ReverseMap();

            CreateMap<Order, CheckoutOrderCommand>().ReverseMap();

            CreateMap<Order, UpdateOrderCommand>().ReverseMap();
        }
    }
}
