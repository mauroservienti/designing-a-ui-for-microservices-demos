using Microsoft.AspNetCore.Mvc;

namespace Divergent.Sales.ViewComponents
{
    [ViewComponent(Name = "Divergent.Sales.ViewComponents.SalesOrderDetails")]
    public class OrderDetailsViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(dynamic viewModel)
        {
            return View(viewModel);
        }
    }
}
