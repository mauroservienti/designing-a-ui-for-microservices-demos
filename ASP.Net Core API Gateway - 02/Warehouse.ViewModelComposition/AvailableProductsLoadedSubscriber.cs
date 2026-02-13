using JsonUtils;
using Catalog.ViewModelComposition.Events;
using ServiceComposer.AspNetCore;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Warehouse.ViewModelComposition
{
    public class AvailableProductsLoadedSubscriber(IHttpClientFactory httpClientFactory) : ICompositionEventsHandler<AvailableProductsLoaded>
    {
        public async Task Handle(AvailableProductsLoaded @event, HttpRequest request)
        {
            var ids = string.Join(",", @event.AvailableProductsViewModel.Keys);

            var url = $"/api/inventory/products/{ids}";
            var httpClient = httpClientFactory.CreateClient(typeof(AvailableProductsLoadedSubscriber).FullName!);
            var response = await httpClient.GetAsync(url);

            dynamic[] stockItems = await response.Content.AsExpandoArray();

            foreach (dynamic stockItem in stockItems)
            {
                @event.AvailableProductsViewModel[(int)stockItem.Id].Inventory = stockItem.Inventory;
            }
        }
    }
}
