using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.StorageClient;
using System.Diagnostics;

namespace AzureLibrary
{
    public class ContainerUtilities
    {
        private CloudStorageAccount account;

        public ContainerUtilities(CloudStorageAccount account)
        {
            this.account = account;
        }

        public ContainerUtilities(string connstr)
        {
            this.account = CloudStorageAccount.Parse(connstr);
        }

        public CloudBlobContainer CreateContainerIfNotExists(string contname)
        {
            CloudBlobClient blobStorage = this.account.CreateCloudBlobClient();
            CloudBlobContainer container = blobStorage.GetContainerReference(contname);

            Trace.WriteLine("Creating container...", "Information");

            bool containerCreated = false;
            while (!containerCreated)
            {
                try
                {
                    container.CreateIfNotExist();

                    var permissions = container.GetPermissions();

                    permissions.PublicAccess = BlobContainerPublicAccessType.Container;

                    container.SetPermissions(permissions);

                    permissions = container.GetPermissions();

                    containerCreated = true;
                }
                catch (StorageClientException e)
                {
                    if (e.ErrorCode == StorageErrorCode.TransportError)
                    {
                        Trace.TraceError(string.Format("Connect failure! The most likely reason is that the local " +
                            "Development Storage tool is not running or your storage account configuration is incorrect. " +
                            "Message: '{0}'", e.Message));
                        System.Threading.Thread.Sleep(5000);
                    }
                    else
                    {
                        throw;
                    }
                }
            }

            return container;
        }
    }
}
