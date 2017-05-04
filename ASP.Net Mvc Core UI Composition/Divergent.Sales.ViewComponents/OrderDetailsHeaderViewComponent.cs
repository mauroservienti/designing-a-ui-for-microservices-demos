using Microsoft.AspNetCore.Mvc;

namespace Divergent.Sales.ViewComponents
{
    [ViewComponent(Name = "Divergent.Sales.ViewComponents.OrderDetailsHeader")]
    public class OrderDetailsHeaderViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(dynamic viewModel)
        {
            return View(viewModel);
        }
    }
}
