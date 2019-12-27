using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using ServiceComposer.AspNetCore;
using ServiceComposer.AspNetCore.Gateway;

namespace CompositionGateway
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRouting();
            services.AddViewModelComposition();
            services.AddAuthorization(options =>
            {
                options.AddPolicy("APolicy", policy =>
                    policy.Requirements.Add(new SamplePolicy()));
            });

            services.AddSingleton<IAuthorizationHandler, SamplePolicyHandler>();
        }

        public void Configure(IApplicationBuilder app, ILoggerFactory loggerFactory)
        {
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(builder =>
            {
                builder.MapGet("{controller}/{id:int}", Composition.HandleRequest)
                    .RequireAuthorization("APolicy");
            });
        }
    }
}
