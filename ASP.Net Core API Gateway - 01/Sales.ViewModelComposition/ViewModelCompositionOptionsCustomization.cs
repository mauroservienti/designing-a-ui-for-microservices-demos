using System;
using Microsoft.Extensions.DependencyInjection;
using ServiceComposer.AspNetCore;

namespace Sales.ViewModelComposition
{
    public class ViewModelCompositionOptionsCustomization : IViewModelCompositionOptionsCustomization
    {
        public void Customize(ViewModelCompositionOptions options)
        {
            options.AddServicesConfigurationHandler(typeof(ProductDetailsGetHandler), (type, services) =>
            {
                services.AddHttpClient<ProductDetailsGetHandler>(typeof(ProductDetailsGetHandler).FullName, client =>
                {
                    client.BaseAddress = new Uri("http://localhost:5001");
                });
            });
        }
    }
}