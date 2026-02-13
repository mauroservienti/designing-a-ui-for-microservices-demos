using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using ServiceComposer.AspNetCore;
using System.Net.Http;
using System.Threading.Tasks;
using JsonUtils;
using Microsoft.AspNetCore.Mvc;

namespace Sales.ViewModelComposition.CompositionHandlers
{
    [CompositionHandler]
    class ProductDetailsCompositionHandler(HttpClient client, IHttpContextAccessor httpContextAccessor) 
    {
        [HttpGet("/products/details/{id}")]
        public async Task Handle()
        {
            var request = httpContextAccessor.HttpContext!.Request;
            var id = (string)request.HttpContext.GetRouteData().Values["id"];

            var url = $"/api/prices/product/{id}";
            var response = await client.GetAsync(url);

            dynamic productPrice = await response.Content.AsExpando();

            var vm = request.GetComposedResponseModel();
            vm.ProductPrice = productPrice.Price;
        }
    }
}
