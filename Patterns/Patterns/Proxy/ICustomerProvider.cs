using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Patterns.Proxy
{
    public interface ICustomerProvider
    {
        ICustomer GetCustomer(int id);
    }
}
