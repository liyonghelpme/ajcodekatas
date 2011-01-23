namespace Azure.WebCrawler.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Microsoft.WindowsAzure;
    using Microsoft.WindowsAzure.StorageClient;
    using Azure.WebCrawler.Entities;

    public class DataContext : TableServiceContext
    {
        public const string DomainTableName = "Domains";
        public const string PageTableName = "Pages";

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

        public IQueryable<Domain> Domains
        {
            get
            {
                return this.CreateQuery<Domain>(DomainTableName);
            }
        }

        public IQueryable<Page> Pages
        {
            get
            {
                return this.CreateQuery<Page>(PageTableName);
            }
        }
    }
}
