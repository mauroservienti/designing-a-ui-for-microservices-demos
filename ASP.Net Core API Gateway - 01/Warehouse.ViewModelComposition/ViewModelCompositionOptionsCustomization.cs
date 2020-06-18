using System;
using Microsoft.Extensions.DependencyInjection;
using ServiceComposer.AspNetCore;

namespace Warehouse.ViewModelComposition
{
    public class ViewModelCompositionOptionsCustomization : IViewModelCompositionOptionsCustomization
    {
        public void Customize(ViewModelCompositionOptions options)
        {
            options.AddServicesConfigurationHandler(typeof(ProductDetailsGetHandler), (type, services) =>
            {
                services.AddHttpClient<ProductDetailsGetHandler>(client =>
                {
                    client.BaseAddress = new Uri("http://localhost:5003");
                });
            });
        }
    }
}