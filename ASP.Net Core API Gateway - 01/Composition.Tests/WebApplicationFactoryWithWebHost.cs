using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;

namespace Composition.Tests
{
    public class WebApplicationFactoryWithWebHost<TStartup> :
        WebApplicationFactory<TStartup>
        where TStartup : class
    {
        public Action<IWebHostBuilder> BuilderCustomization { get; set; }

        protected override IWebHostBuilder CreateWebHostBuilder()
        {
            var host = new WebHostBuilder()
                .UseKestrel()
                .UseStartup<TStartup>();

            return host;
        }

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            base.ConfigureWebHost(builder);

            BuilderCustomization?.Invoke(builder);
        }
    }
}