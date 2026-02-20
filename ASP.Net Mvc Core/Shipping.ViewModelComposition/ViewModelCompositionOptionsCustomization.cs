using System;
using Microsoft.Extensions.DependencyInjection;
using ServiceComposer.AspNetCore;

namespace Shipping.ViewModelComposition
{
    public class ViewModelCompositionOptionsCustomization : IViewModelCompositionOptionsCustomization
    {
        public void Customize(ViewModelCompositionOptions options)
        {
            options.AddServicesConfigurationHandler(typeof(CompositionHandlers.ProductDetailsCompositionHandler), (type, services) =>
            {
                services.AddHttpClient<CompositionHandlers.ProductDetailsCompositionHandler>(typeof(CompositionHandlers.ProductDetailsCompositionHandler).FullName, client =>
                {
                    client.BaseAddress = new Uri("http://localhost:5004");
                });
            });
        }
    }
}