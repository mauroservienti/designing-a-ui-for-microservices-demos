using System;
using ConfigurationUtils;
using Microsoft.Extensions.DependencyInjection;
using ServiceComposer.AspNetCore;

namespace Warehouse.ViewModelComposition
{
    public class ViewModelCompositionOptionsCustomization : IViewModelCompositionOptionsCustomization
    {
        private static string baseAddress = "http://localhost:5003";
        public void Customize(ViewModelCompositionOptions options)
        {
            options.RegisterHttpClient<CompositionHandlers.ProductDetailsCompositionHandler>(baseAddress);
            options.RegisterHttpClient<AvailableProductsLoadedSubscriber>(baseAddress);
        }
    }
}