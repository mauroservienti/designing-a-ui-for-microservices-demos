using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using ServiceComposer.AspNetCore;
using System.Net.Http;
using System.Threading.Tasks;
using JsonUtils;
using Microsoft.AspNetCore.Mvc;

namespace Sales.ViewModelComposition
{
    class ProductDetailsGetHandler : ICompositionRequestsHandler
    {
        private readonly HttpClient _httpClient;

        public ProductDetailsGetHandler(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        [HttpGet("/products/details/{id}")]
        public async Task Handle(HttpRequest request)
        {
            var id = (string)request.HttpContext.GetRouteData().Values["id"];

            var url = $"/api/prices/product/{id}";
            var response = await _httpClient.GetAsync(url);

            dynamic productPrice = await response.Content.AsExpando();

            var vm = request.GetComposedResponseModel();
            vm.ProductPrice = productPrice.Price;
        }
    }
}
