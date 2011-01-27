using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Customers.Entities;
using Microsoft.WindowsAzure;
using Customers.Services;

namespace AzureWebCustomers
{
    public partial class CustomerUpdate : System.Web.UI.Page
    {
        public Customer Customer;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                CloudStorageAccount storage = CloudStorageAccount.FromConfigurationSetting("DataConnectionString");
                CustomerServices services = new CustomerServices(new DataContext(storage));
                string id = Request["Id"];
                this.Customer = services.GetCustomerById(id);
                this.DataBind();
            }
        }
    }
}