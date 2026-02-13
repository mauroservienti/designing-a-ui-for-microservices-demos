using System.Net.Http;
using System.Threading.Tasks;
using JsonUtils;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServiceComposer.AspNetCore;

namespace Warehouse.ViewModelComposition.CompositionHandlers;

[CompositionHandler]
public class ProductDetailsCompositionHandler(HttpClient client, IHttpContextAccessor httpContextAccessor)
{
    [HttpGet("/products/details/{id}")]
    public async Task Get(string id)
    {
        var url = $"/api/inventory/product/{id}";
        var response = await client.GetAsync(url);

        dynamic stockItem = await response.Content.AsExpando();

        dynamic vm = httpContextAccessor.HttpContext!.Request.GetComposedResponseModel();
        vm.ProductInventory = stockItem.Inventory;
        vm.ProductOutOfStock = stockItem.Inventory == 0;
    }
}
