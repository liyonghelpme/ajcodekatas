using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Patterns.Proxy
{
    public class InMemoryCustomerProvider : ICustomerProvider
    {
        public ICustomer GetCustomer(int id)
        {
            return new Customer()
            {
                Id = id,
                Name = string.Format("Customer {0}", id),
                Address = string.Format("Address {0}", id),
                Notes = string.Format("Notes {0}", id)
            };
        }
    }
}
