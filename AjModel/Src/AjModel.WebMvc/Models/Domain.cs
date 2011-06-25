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

            this.Persons = new List<Person>();

            for (int k = 1; k < 10; k++)
                this.Persons.Add(CreatePerson(k));
        }

        public IList<Customer> Customers { get; set; }

        public IList<Person> Persons { get; set; }

        private static Customer CreateCustomer(int n)
        {
            return new Customer()
            {
                Id = n,
                Name = string.Format("Customer {0}", n),
                Address = string.Format("Address {0}", n),
                Notes = string.Format("Notes {0}", n)
            };
        }

        private static Person CreatePerson(int n)
        {
            return new Person()
            {
                Id = n,
                FirstName = string.Format("Joe {0}", n),
                LastName = string.Format("Doe {0}", n),
                Age = n*5
            };
        }
    }
}
