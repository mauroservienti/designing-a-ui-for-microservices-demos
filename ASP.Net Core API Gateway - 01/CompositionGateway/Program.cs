using System;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace CompositionGateway
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration(configurationBuilder =>
                {
                    const string searchPattern = "appsettings.*.ViewModelComposition.json";
                    foreach (var file in Directory.EnumerateFiles(AppDomain.CurrentDomain.BaseDirectory, searchPattern))
                    {
                        configurationBuilder.AddJsonFile(file, optional: true, reloadOnChange: true);
                    }
                })
                .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); });
    }
}
