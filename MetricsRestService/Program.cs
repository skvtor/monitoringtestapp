using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Web.Http;
using MetricsCommon;
using MetricsCommon.Configuration;
using MetricsHub;
using Microsoft.Owin.Hosting;
using Newtonsoft.Json;
using Owin;
using Unity;
using Unity.WebApi;

namespace MetricsRestService
{
    class Program
    {
        static MetricsHubService _hub;

        static void Main(string[] args)
        {
            var metricConfig = JsonConvert.DeserializeObject<Configuration>(File.ReadAllText("Config.json"));

            _hub = new MetricsHubService();
            _hub.Start(metricConfig);

            //Process.Start("MetricsMonitor.exe");

            string baseUri = metricConfig.ConfigItems[Constants.Config_ApiUri];
            Console.WriteLine("Starting web Server...");
            WebApp.Start(baseUri, (appBuilder) => {
                HttpConfiguration config = new HttpConfiguration();

                var container = new UnityContainer();
                container.RegisterFactory<MetricsHubService>((obj) => _hub);
                config.DependencyResolver = new UnityDependencyResolver(container);

                config.MapHttpAttributeRoutes();
                appBuilder.UseWebApi(config);
            });
            Console.WriteLine("Please check the URL: {0}/api/metrics/processes", baseUri);
            Console.WriteLine("Press Enter to quit. ");
            Console.ReadLine();
        }
    }
}
