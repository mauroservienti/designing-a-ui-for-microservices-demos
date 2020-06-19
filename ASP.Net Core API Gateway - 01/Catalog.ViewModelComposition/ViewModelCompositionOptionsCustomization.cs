using System;
using Microsoft.Extensions.DependencyInjection;
using ServiceComposer.AspNetCore;

namespace Catalog.ViewModelComposition
{
    public class ViewModelCompositionOptionsCustomization : IViewModelCompositionOptionsCustomization
    {
        public void Customize(ViewModelCompositionOptions options)
        {
            options.AddServicesConfigurationHandler(typeof(ProductDetailsGetHandler), (type, services) =>
            {
                services.AddHttpClient<ProductDetailsGetHandler>(typeof(ProductDetailsGetHandler).FullName, client =>
                {
                    client.BaseAddress = new Uri("http://localhost:5002");
                });
            });
        }
    }
}