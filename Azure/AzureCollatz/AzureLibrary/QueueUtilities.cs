using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.StorageClient;
using System.Diagnostics;

namespace AzureLibrary
{
    public class QueueUtilities
    {
        private CloudStorageAccount account;

        public QueueUtilities(CloudStorageAccount account)
        {
            this.account = account;
        }

        public QueueUtilities(string connstr)
        {
            this.account = CloudStorageAccount.Parse(connstr);
        }

        public CloudQueue CreateQueueIfNotExists(string queuename)
        {
            CloudQueueClient queueStorage = this.account.CreateCloudQueueClient();
            CloudQueue queue = queueStorage.GetQueueReference(queuename);
            
            Trace.WriteLine("Creating queue...", "Information");

            Boolean queuecreated = false;

            while (queuecreated == false)
            {
                try
                {
                    queue.CreateIfNotExist();
                    queuecreated = true;
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

            return queue;
        }
    }
}
