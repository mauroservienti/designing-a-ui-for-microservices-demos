using System;
using Microsoft.Extensions.DependencyInjection;
using ServiceComposer.AspNetCore;
using Shipping.ViewModelComposition.CompositionHandlers;

namespace Shipping.ViewModelComposition
{
    public class ViewModelCompositionOptionsCustomization : IViewModelCompositionOptionsCustomization
    {
        public void Customize(ViewModelCompositionOptions options)
        {
            options.AddServicesConfigurationHandler(typeof(ProductDetailsCompositionHandler), (type, services) =>
            {
                services.AddHttpClient<ProductDetailsCompositionHandler>(typeof(ProductDetailsCompositionHandler).FullName, client =>
                {
                    client.BaseAddress = new Uri("http://localhost:5004");
                });
            });
        }
    }
}