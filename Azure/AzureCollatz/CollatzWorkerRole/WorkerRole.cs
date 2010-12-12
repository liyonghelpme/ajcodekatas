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

            while (true)
            {
                CloudQueueMessage msg = queue.GetMessage();
                if (msg != null)
                    if (ProcessMessage(msg))
                        queue.DeleteMessage(msg);
                    else
                    {
                        Thread.Sleep(10000);
                        Trace.WriteLine("Working", "Information");
                    }
            }
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
