using Microsoft.AspNetCore.Mvc;

namespace Divergent.Sales.ViewComponents
{
    [ViewComponent(Name = "Divergent.Sales.ViewComponents.SalesOrdersList")]
    public class OrdersListViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(dynamic viewModel)
        {
            return View(viewModel);
        }
    }
}
