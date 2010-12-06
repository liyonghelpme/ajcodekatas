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

namespace NumberWorkerRole
{
    public class WorkerRole : RoleEntryPoint
    {
        public override void Run()
        {
            // This is a sample worker implementation. Replace with your logic.
            Trace.WriteLine("NumberWorkerRole entry point called", "Information");

            CloudStorageAccount account = CloudStorageAccount.FromConfigurationSetting("DataConnectionString");

            QueueUtilities qutil = new QueueUtilities(account);
            CloudQueue queue = qutil.CreateQueueIfNotExists("numbers");

            while (true)
            {
                CloudQueueMessage msg = queue.GetMessage();
                if (msg != null)
                {
                    int number = Convert.ToInt32(msg.AsString);
                    Trace.WriteLine(string.Format("Processing number: {0}", number), "Information");
                    number--;
                    if (number > 0)
                    {
                        CloudQueueMessage newmsg = new CloudQueueMessage(number.ToString());
                        queue.AddMessage(newmsg);
                    }

                    queue.DeleteMessage(msg);
                }
                else
                {
                    Thread.Sleep(10000);
                    Trace.WriteLine("Working", "Information");
                }
            }
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
