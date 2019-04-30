using Microsoft.AspNetCore.Mvc;

namespace Shipping.ViewComponents
{
    [ViewComponent(Name = "Shipping.ViewComponents.ProductDetailsShippingOptions")]
    public class ProductDetailsShippingOptionsViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(dynamic viewModel)
        {
            return View(viewModel);
        }
    }
}
