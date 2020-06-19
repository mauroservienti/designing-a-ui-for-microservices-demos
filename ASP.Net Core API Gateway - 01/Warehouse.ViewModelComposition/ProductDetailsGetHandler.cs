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
        readonly HttpClient _client;

        public ProductDetailsGetHandler(HttpClient client)
        {
            _client = client;
        }

        [HttpGet("/products/details/{id}")]
        public async Task Handle(HttpRequest request)
        {
            var id = (string)request.HttpContext.GetRouteData().Values["id"];

            var url = $"/api/inventory/product/{id}";
            var response = await _client.GetAsync(url);

            dynamic stockItem = await response.Content.AsExpando();

            dynamic vm = request.GetComposedResponseModel();
            vm.ProductInventory = stockItem.Inventory;
            vm.ProductOutOfStock = stockItem.Inventory == 0;
        }
    }
}
