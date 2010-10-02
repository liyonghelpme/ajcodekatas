using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AjAgents.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            foreach (string arg in args)
                MakeAgent().Post(resolver => resolver.Process(arg));

            System.Console.ReadLine();
        }

        static Agent<Resolver> MakeAgent()
        {
            Resolver resolver = new Resolver();
            Harvester harvester = new Harvester();
            Downloader downloader = new Downloader();

            Agent<Resolver> aresolver = new Agent<Resolver>(resolver);
            Agent<Harvester> aharvester = new Agent<Harvester>(harvester);
            Agent<Downloader> adownloader = new Agent<Downloader>(downloader);

            resolver.Downloader = adownloader;
            harvester.Resolver = aresolver;
            downloader.Harvester = aharvester;

            return aresolver;
        }
    }
}
