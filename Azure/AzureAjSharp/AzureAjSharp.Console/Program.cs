using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.WindowsAzure.ServiceRuntime;
using System.Configuration;
using Microsoft.WindowsAzure;
using AzureLibrary;
using Microsoft.WindowsAzure.StorageClient;
using System.Threading;

namespace AzureAjSharp.Console
{
    class Program
    {
        static private QueueUtilities qutil;

        static void Main(string[] args)
        {
            Microsoft.WindowsAzure.CloudStorageAccount.SetConfigurationSettingPublisher((configName, configSetter) =>
            {
                string configuration = RoleEnvironment.IsAvailable ?
                    RoleEnvironment.GetConfigurationSettingValue(configName) :
                    ConfigurationManager.AppSettings[configName];

                configSetter(configuration);
            });

            CloudStorageAccount account = CloudStorageAccount.FromConfigurationSetting("DataConnectionString");

            qutil = new QueueUtilities(account);
            CloudQueue queue = qutil.CreateQueueIfNotExists("ajsprograms");

            (new Thread(new ThreadStart(GetResponse))).Start();

            while (true)
            {
                string program = "";

                string line = System.Console.ReadLine();

                while (line != "go")
                {
                    program += line;
                    program += "\r\n";
                    line = System.Console.ReadLine();
                }

                queue.AddMessage(new CloudQueueMessage(program));
            }
        }

        private static void GetResponse()
        {
            CloudQueue qresponse = qutil.CreateQueueIfNotExists("ajsresponses");

            while (true)
            {
                CloudQueueMessage response = qresponse.GetMessage();
                while (response == null)
                {
                    Thread.Sleep(10000);
                    response = qresponse.GetMessage();
                }
                System.Console.WriteLine(response.AsString);
                qresponse.DeleteMessage(response);
            }
        }
    }
}

