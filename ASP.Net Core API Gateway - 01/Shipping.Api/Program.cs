using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Shipping.Api.Data;

namespace Shipping.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            ShippingContext.CreateSeedData();
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); });
    }
}
