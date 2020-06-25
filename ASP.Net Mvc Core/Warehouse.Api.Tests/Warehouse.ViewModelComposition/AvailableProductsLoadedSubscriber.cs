﻿using JsonUtils;
using Catalog.ViewModelComposition.Events;
 using ServiceComposer.AspNetCore;
 using System.Net.Http;
 using Microsoft.AspNetCore.Mvc;

 namespace Warehouse.ViewModelComposition
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

                var url = $"/api/inventory/products/{ids}";
                var response = await _httpClient.GetAsync(url);

                dynamic[] stockItems = await response.Content.AsExpandoArray();

                foreach (dynamic stockItem in stockItems)
                {
                    @event.AvailableProductsViewModel[(int)stockItem.Id].Inventory = stockItem.Inventory;
                }
            });
        }
    }
}