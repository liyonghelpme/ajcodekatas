using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AjModel.Tests.Entities
{
    public class SimpleDomain
    {
        public SimpleDomain()
        {
            this.Customers = new List<Customer>();

            for (int k = 1; k <= 10; k++)
            {
                Customer customer = new Customer()
                {
                    Id = k,
                    Name = string.Format("Customer {0}", k)
                };

                this.Customers.Add(customer);
            }
        }

        public IList<Customer> Customers { get; set; }

        public ProductList Products { get; set; }
    }
}
