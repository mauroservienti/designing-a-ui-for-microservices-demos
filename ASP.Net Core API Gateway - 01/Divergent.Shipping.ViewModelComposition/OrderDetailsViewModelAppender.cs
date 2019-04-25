using JsonUtils;
using ServiceComposer.AspNetCore;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System.Net.Http;
using System.Threading.Tasks;

namespace Divergent.Shipping.ViewModelComposition
{
    public class OrderDetailsViewModelAppender : IHandleRequests
    {
        public bool Matches(RouteData routeData, string httpVerb, HttpRequest request)
        {
            /*
             * matching is a bit weak in this sample, it's designed 
             * this way to satisfy both the Gateway and the Mvc sample
             */
            var controller = (string)routeData.Values["controller"];

            return HttpMethods.IsGet(httpVerb)
                && controller.ToLowerInvariant() == "orders"
                && routeData.Values.ContainsKey("id");
        }

        public async Task Handle(string requestId, dynamic vm, RouteData routeData, HttpRequest request)
        {
            var id = (string)routeData.Values["id"];

            var url = $"http://localhost:20296/api/shippinginfo/order/{id}";
            var client = new HttpClient();
            var response = await client.GetAsync(url);

            dynamic shipping = await response.Content.AsExpando();

            vm.ShippingStatus = shipping.Status;
            vm.ShippingCourier = shipping.Courier;
        }
    }
}