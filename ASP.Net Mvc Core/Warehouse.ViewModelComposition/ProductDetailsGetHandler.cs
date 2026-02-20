using Microsoft.AspNetCore.Http;
using ServiceComposer.AspNetCore;
using JsonUtils;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Warehouse.ViewModelComposition.CompositionHandlers
{
    [CompositionHandler]
    class ProductDetailsCompositionHandler(HttpClient client, IHttpContextAccessor httpContextAccessor)
    {
        [HttpGet("/products/details/{id}")]
        public async Task Handle(string id)
        {
            var request = httpContextAccessor.HttpContext!.Request;

            var url = $"/api/inventory/product/{id}";
            var response = await client.GetAsync(url);

            dynamic stockItem = await response.Content.AsExpando();

            dynamic vm = request.GetComposedResponseModel();
            vm.ProductInventory = stockItem.Inventory;
            vm.ProductOutOfStock = stockItem.Inventory == 0;
        }
    }
}
