using Microsoft.AspNetCore.Mvc;

namespace Divergent.Sales.ViewComponents
{
    [ViewComponent(Name = "Divergent.Sales.ViewComponents.SalesOrdersListHeader")]
    public class OrdersListHeaderViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(dynamic viewModel)
        {
            return View(viewModel);
        }
    }
}
