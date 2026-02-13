using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using ServiceComposer.AspNetCore;
using JsonUtils;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Warehouse.ViewModelComposition.CompositionHandlers
{
    class ProductDetailsCompositionHandler(HttpClient client, IHttpContextAccessor httpContextAccessor) 
    {
        [HttpGet("/products/details/{id}")]
        public async Task Handle()
        {
            var request = httpContextAccessor.HttpContext!.Request;
            var id = (string)request.HttpContext.GetRouteData().Values["id"];

            var url = $"/api/inventory/product/{id}";
            var response = await client.GetAsync(url);

            dynamic stockItem = await response.Content.AsExpando();

            dynamic vm = request.GetComposedResponseModel();
            vm.ProductInventory = stockItem.Inventory;
            vm.ProductOutOfStock = stockItem.Inventory == 0;
        }
    }
}
