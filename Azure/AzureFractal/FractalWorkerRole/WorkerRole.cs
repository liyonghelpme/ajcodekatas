namespace FractalWorkerRole
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Net;
    using System.Threading;
    using AzureLibrary;
    using Microsoft.WindowsAzure;
    using Microsoft.WindowsAzure.Diagnostics;
    using Microsoft.WindowsAzure.ServiceRuntime;
    using Microsoft.WindowsAzure.StorageClient;
    using Fractal.Azure;
    using Fractal;
    using System.IO;

    public class WorkerRole : RoleEntryPoint
    {
        public override void Run()
        {
            // This is a sample worker implementation. Replace with your logic.
            Trace.WriteLine("FractalWorkerRole entry point called", "Information");
            CloudStorageAccount account = CloudStorageAccount.FromConfigurationSetting("DataConnectionString");

            CloudBlobClient blobClient = account.CreateCloudBlobClient();
            CloudBlobContainer blobContainer = blobClient.GetContainerReference("fractalsectors");
            blobContainer.CreateIfNotExist();
            QueueUtilities qutil = new QueueUtilities(account);
            CloudQueue queue = qutil.CreateQueueIfNotExists("fractaltoprocess");
            CloudQueue outqueue = qutil.CreateQueueIfNotExists("fractalsectors");

            Calculator calculator = new Calculator();

            while (true)
            {
                CloudQueueMessage msg = queue.GetMessage();

                if (msg != null)
                {
                    Trace.WriteLine(string.Format("Processing {0}", msg.AsString));
                    SectorInfo info = SectorUtilities.FromMessageToSectorInfo(msg);

                    if (info.Width > 100 || info.Height > 100)
                    {
                        Trace.WriteLine("Splitting message...");
                        for (int x = 0; x < info.Width; x += 100)
                            for (int y = 0; y < info.Height; y += 100)
                            {
                                SectorInfo newinfo = info.Clone();
                                newinfo.FromX = x + info.FromX;
                                newinfo.FromY = y + info.FromY;
                                newinfo.Width = Math.Min(100, info.Width - x);
                                newinfo.Height = Math.Min(100, info.Height - y);
                                CloudQueueMessage newmsg = SectorUtilities.FromSectorInfoToMessage(newinfo);
                                queue.AddMessage(newmsg);
                            }
                    }
                    else
                    {
                        Trace.WriteLine("Processing message...");
                        Sector sector = calculator.CalculateSector(info);
                        string blobname = string.Format("{0}.{1}.{2}.{3}.{4}", info.Id, sector.FromX, sector.FromY, sector.Width, sector.Height);
                        CloudBlob blob = blobContainer.GetBlobReference(blobname);
                        MemoryStream stream = new MemoryStream();
                        BinaryWriter writer =new BinaryWriter(stream);

                        foreach (int value in sector.Values)
                            writer.Write(value);

                        writer.Flush();
                        stream.Seek(0, SeekOrigin.Begin);

                        blob.UploadFromStream(stream);

                        stream.Close();

                        CloudQueueMessage outmsg = new CloudQueueMessage(blobname);
                        outqueue.AddMessage(outmsg);
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
