using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.IO;

namespace DotNetHttpServer
{
    class Program
    {
        static string rootDirectory;

        static void Main(string[] args)
        {
            rootDirectory = args[0];

            HttpListener listener = new HttpListener();

            for (int k = 1; k < args.Length; k++)
                listener.Prefixes.Add(args[k]);

            listener.Start();

            while (true)
            {
                HttpListenerContext context = listener.GetContext();
                Process(context);
            }
        }

        private static void Process(HttpListenerContext context)
        {
            string filename = context.Request.Url.AbsolutePath;
            Console.WriteLine(filename);

            filename = filename.Substring(1);

            if (string.IsNullOrEmpty(filename))
                filename = "index.html";

            filename = Path.Combine(rootDirectory, filename);

            Stream input = new FileStream(filename, FileMode.Open);
            byte[] buffer = new byte[1024*16];
            int nbytes;

            while ((nbytes = input.Read(buffer, 0, buffer.Length)) > 0)
                context.Response.OutputStream.Write(buffer, 0, nbytes);

            input.Close();
            context.Response.OutputStream.Close();
        }
    }
}
