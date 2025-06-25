using System;
using ConfigurationUtils;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ServiceComposer.AspNetCore;
using Shipping.ViewModelComposition.CompositionHandlers;

namespace Shipping.ViewModelComposition
{
    public class ViewModelCompositionOptionsCustomization : IViewModelCompositionOptionsCustomization
    {
        public void Customize(ViewModelCompositionOptions options)
        {
            options.RegisterHttpClient<ProductDetailsCompositionHandler>((serviceProvider, httpClient) =>
            {
                var configuration = serviceProvider.GetService<IConfiguration>();
                var baseAddress = configuration?.GetSection("Shipping:BaseAddress").Value ?? "http://localhost:5004";
                httpClient.BaseAddress = new Uri(baseAddress);
            });
        }
    }
}