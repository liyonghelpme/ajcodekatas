using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Patterns.Proxy
{
    public class DbCustomerProvider : ICustomerProvider
    {
        public ICustomer GetCustomer(int id)
        {
            throw new NotImplementedException();
        }
    }
}
