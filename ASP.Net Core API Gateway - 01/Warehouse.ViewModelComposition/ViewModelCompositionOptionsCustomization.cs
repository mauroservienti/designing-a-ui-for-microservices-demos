using ConfigurationUtils;
using ServiceComposer.AspNetCore;
using Warehouse.ViewModelComposition.CompositionHandlers;

namespace Warehouse.ViewModelComposition
{
    public class ViewModelCompositionOptionsCustomization : IViewModelCompositionOptionsCustomization
    {
        public void Customize(ViewModelCompositionOptions options)
        {
            options.RegisterHttpClient<ProductDetailsCompositionHandler>("http://localhost:5003");
        }
    }
}