using Microsoft.AspNetCore.Mvc;

namespace Catalog.ViewComponents
{
    [ViewComponent(Name = "Catalog.ViewComponents.AvailableProducts")]
    public class AvailableProductsViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(dynamic viewModel)
        {
            return View(viewModel);
        }
    }
}
