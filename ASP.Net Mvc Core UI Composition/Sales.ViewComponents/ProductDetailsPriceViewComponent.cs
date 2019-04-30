using Microsoft.AspNetCore.Mvc;

namespace Sales.ViewComponents
{
    [ViewComponent(Name = "Sales.ViewComponents.ProductDetailsPrice")]
    public class ProductDetailsPriceViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(dynamic viewModel)
        {
            return View(viewModel);
        }
    }
}
