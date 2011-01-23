namespace Azure.WebCrawler.Entities
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Microsoft.WindowsAzure.StorageClient;

    public class Domain : TableServiceEntity
    {
        public Domain()
            : this(string.Empty)
        {
        }

        public Domain(string domain)
            : base(domain, string.Empty)
        {
        }

        public string Description { get; set; }
    }
}
