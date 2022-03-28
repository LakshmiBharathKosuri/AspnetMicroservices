using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Application.Exceptions
{
    /*NotFoundException is used to throw and exception when we dont find the record in DB
      ApplicationException is from the system library. We use this ApplicationException
      as base which accepts a string
    */
    public class NotFoundException: ApplicationException
    {
        public NotFoundException(string name, object key)
           : base($"Entity \"{name}\" ({key}) was not found.")
        {
        }
    }
}
