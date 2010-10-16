using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Patterns.Proxy
{
    public interface ICustomer
    {
        int Id { get; set; }
        string Name { get; set; }
        string Address { get; set; }
        string Notes { get; set; }
    }
}
