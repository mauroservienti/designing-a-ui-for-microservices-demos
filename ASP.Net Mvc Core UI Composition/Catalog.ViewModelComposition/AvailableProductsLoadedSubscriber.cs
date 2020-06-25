using JsonUtils;
using Catalog.ViewModelComposition.Events;
using ServiceComposer.AspNetCore;
using System;
using System.Net.Http;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.ViewModelComposition
{
    class AvailableProductsLoadedSubscriber : ICompositionEventsSubscriber
    {
        private readonly HttpClient _httpClient;

        public AvailableProductsLoadedSubscriber(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        [HttpGet("/")]
        public void Subscribe(ICompositionEventsPublisher publisher)
        {
            publisher.Subscribe<AvailableProductsLoaded>(async (@event, request) =>
            {
                var ids = string.Join(",", @event.AvailableProductsViewModel.Keys);

                var url = $"/api/product-details/products/{ids}";
                var response = await _httpClient.GetAsync(url);

                dynamic[] productDetails = await response.Content.AsExpandoArray();

                foreach (dynamic detail in productDetails)
                {
                    @event.AvailableProductsViewModel[(int)detail.Id].ProductName = detail.Name;
                    @event.AvailableProductsViewModel[(int)detail.Id].ProductDescription = detail.Description;
                }
            });
        }
    }
}