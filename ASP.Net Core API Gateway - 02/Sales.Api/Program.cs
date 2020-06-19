using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Sales.Api.Data;

namespace Sales.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            SalesContext.CreateSeedData();
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); });
    }
}
