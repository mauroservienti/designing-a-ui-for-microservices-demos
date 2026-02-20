using Microsoft.AspNetCore.Http;
using ServiceComposer.AspNetCore;
using System.Net.Http;
using System.Threading.Tasks;
using JsonUtils;
using Microsoft.AspNetCore.Mvc;

namespace Sales.ViewModelComposition.CompositionHandlers
{
    [CompositionHandler]
    class ProductDetailsCompositionHandler(HttpClient httpClient, IHttpContextAccessor httpContextAccessor)
    {
        [HttpGet("/products/details/{id}")]
        public async Task Handle(string id)
        {
            var request = httpContextAccessor.HttpContext!.Request;

            var url = $"/api/prices/product/{id}";
            var response = await httpClient.GetAsync(url);

            dynamic productPrice = await response.Content.AsExpando();

            var vm = request.GetComposedResponseModel();
            vm.ProductPrice = productPrice.Price;
        }
    }
}
