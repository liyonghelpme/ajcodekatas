namespace Azure.WebCrawler.Entities
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Microsoft.WindowsAzure.StorageClient;

    public class Page : TableServiceEntity
    {
        public Page()
            : this(string.Empty, string.Empty)
        {
        }

        public Page(string domain, string page)
            : base(domain, page)
        {
        }

        public string PageID { get; set; }
    }
}
