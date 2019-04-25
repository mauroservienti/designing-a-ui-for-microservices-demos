using ServiceComposer.AspNetCore;
using JsonUtils;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System.Net.Http;
using System.Threading.Tasks;

namespace Divergent.Sales.ViewModelComposition
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

            var url = $"http://localhost:20295/api/orders/{id}";
            var client = new HttpClient();
            var response = await client.GetAsync(url);

            dynamic order = await response.Content.AsExpando();

            vm.OrderNumber = order.Number;
            vm.OrderItemsCount = order.ItemsCount;
        }
    }
}