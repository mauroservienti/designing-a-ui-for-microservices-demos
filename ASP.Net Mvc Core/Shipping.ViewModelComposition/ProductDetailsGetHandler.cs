using Microsoft.AspNetCore.Http;
using ServiceComposer.AspNetCore;
using JsonUtils;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Shipping.ViewModelComposition.CompositionHandlers
{
    [CompositionHandler]
    class ProductDetailsCompositionHandler(HttpClient client, IHttpContextAccessor httpContextAccessor)
    {
        [HttpGet("/products/details/{id}")]
        public async Task Handle(string id)
        {
            var request = httpContextAccessor.HttpContext!.Request;

            var url = $"/api/shipping-options/product/{id}";
            var response = await client.GetAsync(url);

            dynamic productShippingOptions = await response.Content.AsExpando();

            var options = ((IEnumerable<dynamic>)productShippingOptions.Options)
                .Select(o => o.Option)
                .ToArray();

            var vm = request.GetComposedResponseModel();
            vm.ProductShippingOptions = string.Join(", ", options);
        }
    }
}
