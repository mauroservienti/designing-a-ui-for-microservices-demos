using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using ServiceComposer.AspNetCore;
using JsonUtils;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Warehouse.ViewModelComposition
{
    class ProductDetailsGetHandler : ICompositionRequestsHandler
    {
        [HttpGet("/products/{id}")]
        public async Task Handle(HttpRequest request)
        {
            var id = (string)request.HttpContext.GetRouteData().Values["id"];

            var url = $"http://localhost:5003/api/inventory/product/{id}";
            var client = new HttpClient();
            var response = await client.GetAsync(url);

            dynamic stockItem = await response.Content.AsExpando();

            dynamic vm = request.GetComposedResponseModel();
            vm.ProductInventory = stockItem.Inventory;
            vm.ProductOutOfStock = stockItem.Inventory == 0;
        }
    }
}
