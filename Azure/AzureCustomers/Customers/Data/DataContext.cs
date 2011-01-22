namespace Customers.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Microsoft.WindowsAzure;
    using Microsoft.WindowsAzure.StorageClient;
    using Customers.Entities;

    public class DataContext : TableServiceContext
    {
        public const string CustomerTableName = "Customers";

        public DataContext(string baseAddress, StorageCredentials credentials)
            : base(baseAddress, credentials)
        {
            this.IgnoreResourceNotFoundException = true;
        }

        public DataContext(CloudStorageAccount storageAccount)
            : base(storageAccount.TableEndpoint.AbsoluteUri, storageAccount.Credentials)
        {
            this.IgnoreResourceNotFoundException = true;
        }       

        public IQueryable<Customer> Customers
        {
            get
            {
                return this.CreateQuery<Customer>(CustomerTableName);
            }
        }
    }
}
