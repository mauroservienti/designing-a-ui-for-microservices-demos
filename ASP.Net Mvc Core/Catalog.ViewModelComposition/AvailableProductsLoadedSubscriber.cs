using JsonUtils;
using Catalog.ViewModelComposition.Events;
using ServiceComposer.AspNetCore;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.ViewModelComposition
{
    public class AvailableProductsLoadedSubscriber(IHttpClientFactory httpClientFactory) : ICompositionEventsHandler<AvailableProductsLoaded>
    {
        public async Task Handle(AvailableProductsLoaded @event, HttpRequest request)
        {
            var ids = string.Join(",", @event.AvailableProductsViewModel.Keys);

            var url = $"/api/product-details/products/{ids}";
            var httpClient = httpClientFactory.CreateClient(typeof(AvailableProductsLoadedSubscriber).FullName!);
            var response = await httpClient.GetAsync(url);

            dynamic[] productDetails = await response.Content.AsExpandoArray();

            foreach (dynamic detail in productDetails)
            {
                @event.AvailableProductsViewModel[(int)detail.Id].ProductName = detail.Name;
                @event.AvailableProductsViewModel[(int)detail.Id].ProductDescription = detail.Description;
            }
        }
    }
}