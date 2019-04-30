using Microsoft.AspNetCore.Mvc;

namespace Warehouse.ViewComponents
{
    [ViewComponent(Name = "Warehouse.ViewComponents.ProductDetailsInventory")]
    public class ProductDetailsInventoryViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(dynamic viewModel)
        {
            return View(viewModel);
        }
    }
}
