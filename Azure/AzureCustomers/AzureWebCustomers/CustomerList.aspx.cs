using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Customers.Services;
using Customers.Data;

namespace AzureWebCustomers
{
    public partial class CustomerList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack) 
            {
                CustomerServices services = new CustomerServices(new DataContext(WebRole.Instance.StorageAccount));
                this.grdCustomerList.DataSource = services.GetCustomerList();
                this.grdCustomerList.DataBind();
            }
        }
    }
}