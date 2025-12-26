using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using JsonUtils;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServiceComposer.AspNetCore;

namespace Shipping.ViewModelComposition.CompositionHandlers;

public class ProductDetailsCompositionHandler(HttpClient client, IHttpContextAccessor httpContextAccessor)
{
    [HttpGet("/products/details/{id}")]
    public async Task Get(string id)
    {
        var url = $"/api/shipping-options/product/{id}";
        var response = await client.GetAsync(url);

        dynamic productShippingOptions = await response.Content.AsExpando();

        var options = ((IEnumerable<dynamic>)productShippingOptions.Options)
            .Select(o => o.Option)
            .ToArray();

        var vm = httpContextAccessor.HttpContext.Request.GetComposedResponseModel();
        vm.ProductShippingOptions = string.Join(", ", options);
    }
}