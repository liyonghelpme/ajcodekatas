namespace Customers.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Customers.Data;
    using Customers.Entities;

    public class CustomerServices
    {
        private DataContext context;

        public CustomerServices(DataContext context)
        {
            this.context = context;
        }

        public Customer GetCustomerById(string id)
        {
            return this.context.Customers.Where(c => c.PartitionKey == id).SingleOrDefault();
        }

        public IEnumerable<Customer> GetCustomerList()
        {
            return this.context.Customers.ToList().OrderBy(c => c.Name);
        }

        public void AddCustomer(Customer customer)
        {
            this.context.AddObject(DataContext.CustomerTableName, customer);
            this.context.SaveChanges();
        }

        public void UpdateCustomer(Customer customer)
        {
            this.context.AttachTo(DataContext.CustomerTableName, customer, "*");
            this.context.UpdateObject(customer);
            //Customer c = this.GetCustomerById(customer.PartitionKey);
            //c.Name = customer.Name;
            //c.Address = customer.Address;
            //c.Notes = customer.Notes;
            this.context.SaveChanges();
        }

        public void DeleteCustomerById(string id)
        {
            Customer c = this.GetCustomerById(id);
            this.context.DeleteObject(c);
            this.context.SaveChanges();
        }
    }
}

