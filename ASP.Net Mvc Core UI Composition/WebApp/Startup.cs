using ITOps.UIComposition.Mvc;
using ServiceComposer.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace WebApp
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRouting();
            services.AddControllersWithViews()
                .AddRazorRuntimeCompilation()
                .AddUIComposition();
            services.AddViewModelComposition(options=>
            {
                options.EnableCompositionOverControllers();
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseRouting();
            app.UseStaticFiles();
            app.UseEndpoints(builder =>
            {
                builder.MapControllers();
                builder.MapCompositionHandlers();
            });
        }
    }
}