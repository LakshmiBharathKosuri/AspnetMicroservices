using Ordering.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Application.Contracts.Persistance
{
    //Ref : https://www.geeksforgeeks.org/c-sharp-generics-introduction/?ref=gcse

    /* IOrderRepository interface is for the database operations, which will 
       inherit the IAsyncRepository[ which is generic type] and pass the T value as the
       Order entity in IAsyncRepository.
     
       IAsyncRepository take the entity Order in place of "T" while implementation.
    */
    public interface IOrderRepository: IAsyncRepository<Order>
    {
        Task<IEnumerable<Order>> GetOrdersByUserName(string userName);
    }
}
