using Microsoft.AspNetCore.Mvc;

namespace Divergent.Sales.ViewComponents
{
    [ViewComponent(Name = "Divergent.Sales.ViewComponents.OrderDetails")]
    public class OrderDetailsViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(dynamic viewModel)
        {
            return View(viewModel);
        }
    }
}
