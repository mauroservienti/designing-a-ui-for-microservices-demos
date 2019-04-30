using Microsoft.AspNetCore.Mvc;

namespace Sales.ViewComponents
{
    [ViewComponent(Name = "Sales.ViewComponents.AvailableProductPrice")]
    public class AvailableProductPriceViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(dynamic viewModel)
        {
            return View(viewModel);
        }
    }
}
