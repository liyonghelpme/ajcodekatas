using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.StorageClient;

namespace AzureBlobsWeb
{
    public partial class _Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
                this.BindGrid();
        }

        private void BindGrid()
        {
            CloudBlobContainer blobContainer = this.GetContainer();
            blobContainer.CreateIfNotExist();
            BlobContainerPermissions perms = new BlobContainerPermissions();
            perms.PublicAccess = BlobContainerPublicAccessType.Blob;
            blobContainer.SetPermissions(perms);
            grdBlobs.DataSource = blobContainer.ListBlobs();
            grdBlobs.DataBind();
        }

        private CloudBlobContainer GetContainer()
        {
            CloudStorageAccount account = CloudStorageAccount.FromConfigurationSetting("DataConnectionString");
            CloudBlobClient blobClient = account.CreateCloudBlobClient();
            CloudBlobContainer blobContainer = blobClient.GetContainerReference("files");
            return blobContainer;
        }

        protected void btnUpdload_Click(object sender, EventArgs e)
        {
            CloudBlobContainer blobContainer = this.GetContainer();
            CloudBlob blob = blobContainer.GetBlobReference(fluFile.PostedFile.FileName);
            blob.UploadByteArray(fluFile.FileBytes);
            this.BindGrid();
        }
    }
}
