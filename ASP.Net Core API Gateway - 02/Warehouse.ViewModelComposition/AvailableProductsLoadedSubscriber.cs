using JsonUtils;
using Catalog.ViewModelComposition.Events;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using ServiceComposer.AspNetCore;
using System;
using System.Net.Http;

namespace Warehouse.ViewModelComposition
{
    class AvailableProductsLoadedSubscriber : ISubscribeToCompositionEvents
    {
        public bool Matches(RouteData routeData, string httpVerb, HttpRequest request)
        {
            var controller = (string)routeData.Values["controller"];
            var action = (string)routeData.Values["action"];

            return HttpMethods.IsGet(httpVerb)
                   && controller.ToLowerInvariant() == "available"
                   && action.ToLowerInvariant() == "products"
                   && !routeData.Values.ContainsKey("id");
        }

        public void Subscribe(IPublishCompositionEvents publisher)
        {
            publisher.Subscribe<AvailableProductsLoaded>(async (requestId, pageViewModel, @event, rd, req) =>
            {
                var ids = String.Join(",", @event.AvailableProductsViewModel.Keys);

                var url = $"http://localhost:5003/api/inventory/products/{ids}";
                var client = new HttpClient();

                var response = await client.GetAsync(url);

                dynamic[] stockItems = await response.Content.AsExpandoArray();

                foreach (dynamic stockItem in stockItems)
                {
                    @event.AvailableProductsViewModel[(int)stockItem.Id].Inventory = stockItem.Inventory;
                }
            });
        }
    }
}