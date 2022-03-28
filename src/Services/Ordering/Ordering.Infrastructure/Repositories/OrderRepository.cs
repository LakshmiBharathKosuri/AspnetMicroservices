using Microsoft.EntityFrameworkCore;
using Ordering.Application.Contracts.Persistance;
using Ordering.Domain.Entities;
using Ordering.Infrastructure.Persistance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Infrastructure.Repositories
{
    /*
     OrderRepository is inheriting from the RepositoryBase<Order> for CRUD operations.
     OrderRepository is inheriting from the IOrderRepository for get operations

     RepositoryBase is a generic type repository wrapper, which will take the entity parameter.
     from the below code the 'T' is Order entity. when we implment the repository base methods,
     'T' will be replaced by the entity object that was passed from here.
     
     */
    public class OrderRepository : RepositoryBase<Order>, IOrderRepository
    {
        /*
         why do we need to create a constructure here with base inheriting.
         Explnation: OrderRepository is inherited from the RepositoryBase.
         In RepositoryBase class we have dbContext constructor injection.
         So, when we have constructor with parameter in parent class, its sub class
         must have the constructor with base inheritance along with the parameter.
         
         as the parent class is inheriting the dbContext from OrderContext class, chils/ sub
         class must be of same type constructor. This is OOP priciple.
         
         The dbContext Object is injected in parent class and it will be available in child 
         or sub class, with the below line of constructor code.
         
         we have to delcare the dbcontext variable in parent class with protected access. to use
         it in the sub/ child classes.
         
         */

        public OrderRepository(OrderContext dbContext) : base(dbContext)
        {
        }

        //implement the method from OrderRepository
        public async Task<IEnumerable<Order>> GetOrdersByUserName(string userName)
        {
            //_dbContext is from repository base.

            //ToListAsync is from using Microsoft.EntityFrameworkCore
            var orderList=await _dbContext.Orders.
                                    Where(o => o.UserName == userName).ToListAsync();
            return orderList;
        }
    }
}
