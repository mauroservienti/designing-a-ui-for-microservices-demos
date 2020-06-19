using JsonUtils;
using Catalog.ViewModelComposition.Events;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using ServiceComposer.AspNetCore;
using System;
using System.Net.Http;
using Microsoft.AspNetCore.Mvc;

namespace Sales.ViewModelComposition
{
    class AvailableProductsLoadedSubscriber : ICompositionEventsSubscriber
    {
        private readonly HttpClient _httpClient;

        public AvailableProductsLoadedSubscriber(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        [HttpGet("/available/products")]
        public void Subscribe(ICompositionEventsPublisher publisher)
        {
            publisher.Subscribe<AvailableProductsLoaded>(async (@event, request) =>
            {
                var ids = String.Join(",", @event.AvailableProductsViewModel.Keys);

                var url = $"/api/prices/products/{ids}";
                var response = await _httpClient.GetAsync(url);

                dynamic[] productPrices = await response.Content.AsExpandoArray();

                foreach (dynamic productPrice in productPrices)
                {
                    @event.AvailableProductsViewModel[(int)productPrice.Id].ProductPrice = productPrice.Price;
                }
            });
        }
    }
}