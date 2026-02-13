using JsonUtils;
using Catalog.ViewModelComposition.Events;
using ServiceComposer.AspNetCore;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Sales.ViewModelComposition
{
    public class AvailableProductsLoadedSubscriber(IHttpClientFactory httpClientFactory) : ICompositionEventsHandler<AvailableProductsLoaded>
    {
        public async Task Handle(AvailableProductsLoaded @event, HttpRequest request)
        {
            var ids = string.Join(",", @event.AvailableProductsViewModel.Keys);

            var url = $"/api/prices/products/{ids}";
            var httpClient = httpClientFactory.CreateClient(typeof(AvailableProductsLoadedSubscriber).FullName!);
            var response = await httpClient.GetAsync(url);

            dynamic[] productPrices = await response.Content.AsExpandoArray();

            foreach (dynamic productPrice in productPrices)
            {
                @event.AvailableProductsViewModel[(int)productPrice.Id].ProductPrice = productPrice.Price;
            }
        }
    }
}
