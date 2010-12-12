using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Threading;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.Diagnostics;
using Microsoft.WindowsAzure.ServiceRuntime;
using Microsoft.WindowsAzure.StorageClient;
using AzureLibrary;
using System.Text;

namespace CollatzWorkerRole
{
    public class WorkerRole : RoleEntryPoint
    {
        public override void Run()
        {
            // This is a sample worker implementation. Replace with your logic.
            Trace.WriteLine("CollatzWorkerRole entry point called", "Information");

            CloudStorageAccount account = CloudStorageAccount.FromConfigurationSetting("DataConnectionString");

            QueueUtilities qutil = new QueueUtilities(account);
            CloudQueue queue = qutil.CreateQueueIfNotExists("numbers");

            CloudQueueClient qclient = account.CreateCloudQueueClient();

            for (int k=0; k<11; k++) 
            {
                CloudQueue q = qclient.GetQueueReference("numbers");
                MessageProcessor p = new MessageProcessor(q, this.ProcessMessage);
                p.Start();
            }

            MessageProcessor processor = new MessageProcessor(queue, this.ProcessMessage);

            processor.Run();
        }

        private bool ProcessMessage(CloudQueueMessage msg)
        {
            int number = Convert.ToInt32(msg.AsString);
            List<int> numbers = new List<int>() { number };

            while (number > 1)
            {
                if ((number % 2) == 0)
                {
                    number = number / 2;
                    numbers.Add(number);
                }
                else
                {
                    number = number * 3 + 1;
                    numbers.Add(number);
                }
            }

            StringBuilder builder = new StringBuilder();

            builder.Append("Result:");
            foreach (int n in numbers)
            {
                builder.Append(" ");
                builder.Append(n);
            }

            Trace.WriteLine(builder.ToString(), "Information");

            return true;
        }

        public override bool OnStart()
        {
            // Set the maximum number of concurrent connections 
            ServicePointManager.DefaultConnectionLimit = 12;

            DiagnosticMonitor.Start("DiagnosticsConnectionString");

            // For information on handling configuration changes
            // see the MSDN topic at http://go.microsoft.com/fwlink/?LinkId=166357.
            RoleEnvironment.Changing += RoleEnvironmentChanging;

            Microsoft.WindowsAzure.CloudStorageAccount.SetConfigurationSettingPublisher((configName, configSetter) =>
            {
                configSetter(Microsoft.WindowsAzure.ServiceRuntime.RoleEnvironment.GetConfigurationSettingValue(configName));
            });

            return base.OnStart();
        }

        private void RoleEnvironmentChanging(object sender, RoleEnvironmentChangingEventArgs e)
        {
            // If a configuration setting is changing
            if (e.Changes.Any(change => change is RoleEnvironmentConfigurationSettingChange))
            {
                // Set e.Cancel to true to restart this role instance
                e.Cancel = true;
            }
        }
    }
}
