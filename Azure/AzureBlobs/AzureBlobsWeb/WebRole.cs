using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.Diagnostics;
using Microsoft.WindowsAzure.ServiceRuntime;
using System.Configuration;
using Microsoft.WindowsAzure.StorageClient;

namespace AzureBlobsWeb
{
    public class WebRole : RoleEntryPoint
    {
        public override bool OnStart()
        {
            Microsoft.WindowsAzure.CloudStorageAccount.SetConfigurationSettingPublisher((configName, configSetter) =>
            {
                string configuration = RoleEnvironment.IsAvailable ?
                    RoleEnvironment.GetConfigurationSettingValue(configName) :
                    ConfigurationManager.AppSettings[configName];

                configSetter(configuration);
            });

            CloudStorageAccount account = CloudStorageAccount.FromConfigurationSetting("DataConnectionString");
            CloudBlobClient blobClient = account.CreateCloudBlobClient();
            CloudBlobContainer blobContainer = blobClient.GetContainerReference("files");
            blobContainer.CreateIfNotExist();
            BlobContainerPermissions perms = new BlobContainerPermissions();
            perms.PublicAccess = BlobContainerPublicAccessType.Blob;
            blobContainer.SetPermissions(perms);

            // For information on handling configuration changes
            // see the MSDN topic at http://go.microsoft.com/fwlink/?LinkId=166357.

            return base.OnStart();
        }
    }
}
