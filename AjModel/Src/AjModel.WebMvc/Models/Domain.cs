using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AjModel.WebMvc.Models
{
    public class Domain
    {
        public static Domain Instance = new Domain();

        public Domain()
        {
            this.Customers = new List<Customer>();

            for (int k = 1; k < 10; k++)
                this.Customers.Add(CreateCustomer(k));
        }

        public IList<Customer> Customers { get; set; }

        private static Customer CreateCustomer(int n)
        {
            return new Customer()
            {
                Id = n,
                Name = string.Format("Customer " + n),
                Address = string.Format("Address " + n),
                Notes = string.Format("Notes " + n)
            };
        }
    }
}
