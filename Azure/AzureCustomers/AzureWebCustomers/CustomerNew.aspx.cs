using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Customers.Entities;
using Customers.Services;
using Customers.Data;
using Microsoft.WindowsAzure;

namespace AzureWebCustomers
{
    public partial class CustomerNew : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnAccept_Click(object sender, EventArgs e)
        {
            Customer customer = new Customer()
            {
                Name = txtName.Text,
                Address = txtAddress.Text
            };

            CloudStorageAccount storage = CloudStorageAccount.FromConfigurationSetting("DataConnectionString");
            CustomerServices service = new CustomerServices(new DataContext(storage));
            
            service.AddCustomer(customer);
            Response.Redirect("~/CustomerList.aspx");
        }
    }
}