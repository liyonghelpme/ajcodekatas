using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Patterns.Proxy
{
    public class CustomerProxy : ICustomer
    {
        private int id;
        ICustomer customer;
        ICustomerProvider provider;

        public CustomerProxy(int id)
            : this(id, new InMemoryCustomerProvider())
        {
        }

        public CustomerProxy(int id, ICustomerProvider provider)
        {
            this.id = id;
            this.provider = provider;
        }

        public int Id { get { return this.id; } set { this.id = value; } }

        public string Name
        {
            get
            {
                this.CheckCustomer();
                return this.customer.Name;
            }

            set
            {
                this.CheckCustomer();
                this.customer.Name = value;
            }
        }

        public string Address
        {
            get
            {
                this.CheckCustomer();
                return this.customer.Address;
            }

            set
            {
                this.CheckCustomer();
                this.customer.Address = value;
            }
        }

        public string Notes
        {
            get
            {
                this.CheckCustomer();
                return this.customer.Notes;
            }

            set
            {
                this.CheckCustomer();
                this.customer.Notes = value;
            }
        }

        private void CheckCustomer()
        {
            if (this.customer != null && this.customer.Id == this.id)
                return;

            this.customer = this.provider.GetCustomer(this.id);
        }
    }
}
