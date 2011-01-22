using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.WindowsAzure.StorageClient;

namespace AzureLibrary
{
    public class MessageQueue
    {
        private CloudQueue queue;

        public MessageQueue(CloudQueue queue)
        {
            this.queue = queue;
        }

        public CloudQueueMessage GetMessage()
        {
            lock (this.queue)
                return this.queue.GetMessage();
        }

        public void DeleteMessage(CloudQueueMessage msg)
        {
            lock (this.queue)
                this.queue.DeleteMessage(msg);
        }
    }
}
