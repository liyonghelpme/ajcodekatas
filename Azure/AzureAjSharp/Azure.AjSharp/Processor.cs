using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.StorageClient;
using AzureLibrary;
using AjSharp;
using System.IO;
using AjSharp.Compiler;
using AjLanguage.Commands;
using System.Threading;
using AjSharp.Primitives;

namespace Azure.AjSharp
{
    public class Processor
    {
        public const string RequestsQueueName = "ajsrequests";
        public const string ResponsesQueueName = "ajsresponses";
        public const string FilesContainerName = "ajsfiles";

        private CloudStorageAccount account;
        private CloudQueue requests;
        private CloudQueue responses;
        private CloudBlobContainer files;

        public Processor(CloudStorageAccount account)
            : this(account, Processor.RequestsQueueName, Processor.ResponsesQueueName, Processor.FilesContainerName)
        {
        }

        public Processor(CloudStorageAccount account, string requestsQueueName, string responsesQueueName, string filesContainerName)
        {
            this.account = account;
            QueueUtilities qutil = new QueueUtilities(account);
            this.requests = qutil.CreateQueueIfNotExists(requestsQueueName);
            this.responses = qutil.CreateQueueIfNotExists(responsesQueueName);
            CloudBlobClient blobStorage = this.account.CreateCloudBlobClient();
            this.files = blobStorage.GetContainerReference(filesContainerName);
        }

        public void Start()
        {
            MessageProcessor p = new MessageProcessor(this.requests, this.ProcessMessage);
            p.Start();
        }

        public void SendRequest(string request)
        {
            this.requests.AddMessage(new CloudQueueMessage(request));
        }

        public string GetResponse()
        {
            CloudQueueMessage response = this.responses.GetMessage();

            while (response == null)
            {
                Thread.Sleep(10000);
                response = this.responses.GetMessage();
            }

            string result = response.AsString;

            this.responses.DeleteMessage(response);

            return result;
        }

        private bool ProcessMessage(CloudQueueMessage msg)
        {
            string message = msg.AsString;

            AjSharpMachine machine = new AjSharpMachine();
            machine.Environment.SetValue("Include", new IncludeBlobSubroutine(this.files));

            StringWriter writer = new StringWriter();
            machine.Out = writer;

            try
            {
                Parser parser = new Parser(message);
                ICommand command;

                while ((command = parser.ParseCommand()) != null)
                    command.Execute(machine.Environment);
            }
            catch (Exception ex)
            {
                writer.WriteLine(ex.Message);
                writer.WriteLine(ex.StackTrace);
            }

            writer.Close();
            this.responses.AddMessage(new CloudQueueMessage(writer.ToString()));

            return true;
        }
    }
}

