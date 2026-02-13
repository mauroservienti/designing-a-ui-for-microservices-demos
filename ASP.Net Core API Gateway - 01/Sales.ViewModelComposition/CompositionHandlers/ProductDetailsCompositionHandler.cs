using System.Net.Http;
using System.Threading.Tasks;
using JsonUtils;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServiceComposer.AspNetCore;

namespace Sales.ViewModelComposition.CompositionHandlers;

public class ProductDetailsCompositionHandler(HttpClient httpClient, IHttpContextAccessor httpContextAccessor)
{
    [HttpGet("/products/details/{id}")]
    public async Task Get(string id)
    {
        var url = $"/api/prices/product/{id}";
        var response = await httpClient.GetAsync(url);

        dynamic productPrice = await response.Content.AsExpando();

        var vm = httpContextAccessor.HttpContext!.Request.GetComposedResponseModel();
        vm.ProductPrice = productPrice.Price;
    }
}
