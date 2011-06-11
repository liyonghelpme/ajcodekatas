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
using AjSharp;
using System.IO;
using AjSharp.Compiler;
using AjLanguage.Commands;
using System.Configuration;

namespace AzureAjSharp.WorkerRole
{
    public class WorkerRole : RoleEntryPoint
    {
        private CloudQueue qresponse;

        public override void Run()
        {
            // This is a sample worker implementation. Replace with your logic.
            Trace.WriteLine("AzureAjSharp.WorkerRole entry point called", "Information");

            CloudStorageAccount account = CloudStorageAccount.FromConfigurationSetting("DataConnectionString");

            QueueUtilities qutil = new QueueUtilities(account);
            CloudQueue queue = qutil.CreateQueueIfNotExists("ajsprograms");
            this.qresponse = qutil.CreateQueueIfNotExists("ajsresponses");

            MessageProcessor p = new MessageProcessor(queue, this.ProcessMessage);
            p.Start();

            while (true)
            {
                Thread.Sleep(10000);
                Trace.WriteLine("Working", "Information");
            }
        }

        private bool ProcessMessage(CloudQueueMessage msg)
        {
            string message = msg.AsString;

            AjSharpMachine machine = new AjSharpMachine();
            StringWriter writer = new StringWriter();
            machine.Out = writer;

            Parser parser = new Parser(message);
            ICommand command;

            while ((command = parser.ParseCommand()) != null)
                command.Execute(machine.Environment);

            writer.Close();
            this.qresponse.AddMessage(new CloudQueueMessage(writer.ToString()));

            return true;
        }

        public override bool OnStart()
        {
            // Set the maximum number of concurrent connections 
            ServicePointManager.DefaultConnectionLimit = 12;

            // For information on handling configuration changes
            // see the MSDN topic at http://go.microsoft.com/fwlink/?LinkId=166357.
            RoleEnvironment.Changing += RoleEnvironmentChanging;

            Microsoft.WindowsAzure.CloudStorageAccount.SetConfigurationSettingPublisher((configName, configSetter) =>
            {
                string configuration = RoleEnvironment.IsAvailable ?
                    RoleEnvironment.GetConfigurationSettingValue(configName) :
                    ConfigurationManager.AppSettings[configName];

                configSetter(configuration);
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
