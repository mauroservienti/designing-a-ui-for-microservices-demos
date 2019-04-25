using Divergent.Sales.ViewModelComposition.Events;
using ServiceComposer.AspNetCore;
using JsonUtils;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System;
using System.Net.Http;

namespace Divergent.Shipping.ViewModelComposition
{
    public class OrdersLoadedSubscriber : ISubscribeToCompositionEvents
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
                && !routeData.Values.ContainsKey("id");
        }

        public void Subscribe(IPublishCompositionEvents publisher)
        {
            publisher.Subscribe<OrdersLoaded>(async (requestId, pageViewModel, @event, routeData, query) =>
            {
                var ids = String.Join(",", @event.OrdersViewModel.Keys);

                var url = $"http://localhost:20296/api/shippinginfo/orders?ids={ids}";
                var client = new HttpClient();

                var response = await client.GetAsync(url);

                dynamic[] shippingInfos = await response.Content.AsExpandoArray();

                foreach (dynamic item in shippingInfos)
                {
                    @event.OrdersViewModel[item.OrderId].ShippingStatus = item.Status;
                    @event.OrdersViewModel[item.OrderId].ShippingCourier = item.Courier;
                }
            });
        }
    }
}