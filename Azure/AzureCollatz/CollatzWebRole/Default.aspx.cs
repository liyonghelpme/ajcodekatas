using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.WindowsAzure.StorageClient;

namespace CollatzWebRole
{
    public partial class _Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnProcess_Click(object sender, EventArgs e)
        {
            int from = Convert.ToInt32(txtFromNumber.Text);
            int to = Convert.ToInt32(txtToNumber.Text);

            for (int k=from; k<=to; k++) 
            {
                CloudQueueMessage msg = new CloudQueueMessage(k.ToString());
                WebRole.Instance.NumbersQueue.AddMessage(msg);
            }
        }
    }
}
