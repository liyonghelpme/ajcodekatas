namespace Customers.Entities
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Microsoft.WindowsAzure.StorageClient;

    public class Customer : TableServiceEntity
    {
        public Customer()
            : this(Guid.NewGuid().ToString())
        {
        }

        public Customer(string id)
            : base(id, string.Empty)
        {
        }

        public string Name { get; set; }

        public string Address { get; set; }

        public string Notes { get; set; }
    }
}
