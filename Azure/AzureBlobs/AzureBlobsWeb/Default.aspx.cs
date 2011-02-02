using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.StorageClient;
using System.IO;

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
            grdBlobs.DataSource = blobContainer.ListBlobs(new BlobRequestOptions()
              {
                  UseFlatBlobListing = true,
                  BlobListingDetails = BlobListingDetails.All
              });
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
            string extension = Path.GetExtension(fluFile.PostedFile.FileName);
            
            if (extension == "jpg")
                blob.Properties.ContentType = "image/jpeg";
            if (extension == "gif")
                blob.Properties.ContentType = "image/gif";
            if (extension == "png")
                blob.Properties.ContentType = "image/png";

            blob.UploadByteArray(fluFile.FileBytes);

            this.BindGrid();
        }
    }
}
