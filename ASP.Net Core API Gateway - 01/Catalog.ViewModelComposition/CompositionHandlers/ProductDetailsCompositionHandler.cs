using System.Net.Http;
using System.Threading.Tasks;
using JsonUtils;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServiceComposer.AspNetCore;

namespace Catalog.ViewModelComposition.CompositionHandlers;

[CompositionHandler]
public class ProductDetailsCompositionHandler(HttpClient httpClient, IHttpContextAccessor httpContextAccessor)
{
    [HttpGet("/products/details/{id}")]
    public async Task Get(string id)
    {
        var url = $"/api/product-details/product/{id}";
        var response = await httpClient.GetAsync(url);

        dynamic details = await response.Content.AsExpando();

        var vm = httpContextAccessor.HttpContext!.Request.GetComposedResponseModel();
        vm.ProductName = details.Name;
        vm.ProductDescription = details.Description;
    }
}
