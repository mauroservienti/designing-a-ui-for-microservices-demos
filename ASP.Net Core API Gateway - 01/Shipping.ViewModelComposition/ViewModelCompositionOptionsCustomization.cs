using System;
using ConfigurationUtils;
using Microsoft.Extensions.DependencyInjection;
using ServiceComposer.AspNetCore;

namespace Shipping.ViewModelComposition
{
    public class ViewModelCompositionOptionsCustomization : IViewModelCompositionOptionsCustomization
    {
        public void Customize(ViewModelCompositionOptions options)
        {
            options.RegisterHttpClient<ProductDetailsGetHandler>("http://localhost:5004");
        }
    }
}