using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AjModel.Tests.Entities
{
    public class SimpleDomain
    {
        public IList<Customer> Customers { get; set; }

        public ProductList Products { get; set; }
    }
}
