using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.WindowsAzure.StorageClient;
using Microsoft.WindowsAzure;
using System.Diagnostics;
using System.Threading;

namespace AzureLibrary
{
    public class MessageProcessor
    {
        private CloudQueue queue;
        private Func<CloudQueueMessage, bool> process;

        public MessageProcessor(CloudQueue queue)
            : this(queue, null)
        {
        }

        public MessageProcessor(CloudQueue queue, Func<CloudQueueMessage, bool> process)
        {
            this.queue = queue;
            this.process = process;
        }

        public void Start()
        {
            Thread thread = new Thread(new ThreadStart(this.Run));
            thread.Start();
        }

        public void Run()
        {
            while (true)
            {
                try
                {
                    CloudQueueMessage msg = this.queue.GetMessage();

                    if (this.ProcessMessage(msg))
                        this.queue.DeleteMessage(msg);
                }
                catch (Exception ex)
                {
                    Trace.WriteLine(ex.Message, "Error");
                }
            }
        }

        public virtual bool ProcessMessage(CloudQueueMessage msg)
        {
            if (msg != null && this.process != null)
                return this.process(msg);

            Trace.WriteLine("Working", "Information");
            Thread.Sleep(10000);

            return false;
        }
    }
}
