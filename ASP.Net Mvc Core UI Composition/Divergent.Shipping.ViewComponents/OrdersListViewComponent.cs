using Microsoft.AspNetCore.Mvc;

namespace Divergent.Shipping.ViewComponents
{
    [ViewComponent(Name = "Divergent.Shipping.ViewComponents.ShippingOrdersList")]
    public class OrdersListViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(dynamic viewModel)
        {
            return View(viewModel);
        }
    }
}
