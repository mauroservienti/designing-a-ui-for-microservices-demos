using Microsoft.AspNetCore.Mvc;

namespace Divergent.Shipping.ViewComponents
{
    [ViewComponent(Name = "Divergent.Shipping.ViewComponents.OrdersListHeader")]
    public class OrdersListHeaderViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(dynamic viewModel)
        {
            return View(viewModel);
        }
    }
}
