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
using Azure.AjSharp;

namespace AzureAjSharp.Console
{
    class Program
    {
        private static Processor Processor;

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

            Processor = new Processor(account);

            (new Thread(new ThreadStart(GetResponse))).Start();

            while (true)
            {
                string program = "";

                string line = System.Console.ReadLine();

                while (line != "send")
                {
                    program += line;
                    program += "\r\n";
                    line = System.Console.ReadLine();
                }

                Processor.SendRequest(program);
            }
        }

        private static void GetResponse()
        {
            while (true)
            {
                System.Console.WriteLine(Processor.GetResponse());
            }
        }
    }
}

