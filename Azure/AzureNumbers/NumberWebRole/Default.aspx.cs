﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.WindowsAzure.StorageClient;
using Microsoft.WindowsAzure;

namespace NumberWebRole
{
    public partial class _Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnProcess_Click(object sender, EventArgs e)
        {
            int number = Convert.ToInt32(txtNumber.Text);
            CloudQueueMessage msg = new CloudQueueMessage(number.ToString());
            CloudStorageAccount account = CloudStorageAccount.FromConfigurationSetting("DataConnectionString");
            CloudQueue numbersQueue = account.CreateCloudQueueClient().GetQueueReference("numbers");
            numbersQueue.AddMessage(msg);
        }
    }
}
