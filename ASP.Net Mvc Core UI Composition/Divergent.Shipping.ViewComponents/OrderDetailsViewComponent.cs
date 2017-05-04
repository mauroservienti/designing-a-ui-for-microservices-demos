using Microsoft.AspNetCore.Mvc;

namespace Divergent.Shipping.ViewComponents
{
    [ViewComponent(Name = "Divergent.Shipping.ViewComponents.ShippingOrderDetails")]
    public class OrderDetailsViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(dynamic viewModel)
        {
            return View(viewModel);
        }
    }
}
