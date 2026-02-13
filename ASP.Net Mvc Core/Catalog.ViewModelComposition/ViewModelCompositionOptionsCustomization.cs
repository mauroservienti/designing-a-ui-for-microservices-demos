using System;
using ConfigurationUtils;
using Microsoft.Extensions.DependencyInjection;
using ServiceComposer.AspNetCore;

namespace Catalog.ViewModelComposition
{
    public class ViewModelCompositionOptionsCustomization : IViewModelCompositionOptionsCustomization
    {
        static string baseAddress = "http://localhost:5002";

        public void Customize(ViewModelCompositionOptions options)
        {
            options.RegisterHttpClient<CompositionHandlers.ProductDetailsCompositionHandler>(baseAddress);
            options.RegisterHttpClient<CompositionHandlers.AvailableProductsCompositionHandler>(baseAddress);
            options.RegisterHttpClient<AvailableProductsLoadedSubscriber>(baseAddress);
        }
    }
}