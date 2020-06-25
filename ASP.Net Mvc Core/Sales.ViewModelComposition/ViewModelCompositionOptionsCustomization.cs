using System;
using ConfigurationUtils;
using Microsoft.Extensions.DependencyInjection;
using ServiceComposer.AspNetCore;

namespace Sales.ViewModelComposition
{
    public class ViewModelCompositionOptionsCustomization : IViewModelCompositionOptionsCustomization
    {
        static string baseAddress = "http://localhost:5001";

        public void Customize(ViewModelCompositionOptions options)
        {
            options.RegisterHttpClient<ProductDetailsGetHandler>(baseAddress);
            options.RegisterHttpClient<AvailableProductsLoadedSubscriber>(baseAddress);
        }
    }
}