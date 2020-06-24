using Catalog.ViewModelComposition.Events;
using Microsoft.AspNetCore.Http;
using ServiceComposer.AspNetCore;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using JsonUtils;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.ViewModelComposition
{
    class AvailableProductsGetHandler : ICompositionRequestsHandler
    {
        private readonly HttpClient _httpClient;

        public AvailableProductsGetHandler(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        [HttpGet("/home/index")]
        public async Task Handle(HttpRequest request)
        {
            var url = $"/api/available/products";
            var response = await _httpClient.GetAsync(url);

            var availableProducts = await response.Content.As<int[]>();
            var availableProductsViewModel = MapToDictionary(availableProducts);

            var vm = request.GetComposedResponseModel();
            await vm.RaiseEvent(new AvailableProductsLoaded()
            {
                AvailableProductsViewModel = availableProductsViewModel
            });

            vm.AvailableProducts = availableProductsViewModel.Values.ToList();
        }

        IDictionary<int, dynamic> MapToDictionary(IEnumerable<int> availableProducts)
        {
            var availableProductsViewModel = new Dictionary<int, dynamic>();

            foreach (var id in availableProducts)
            {
                dynamic vm = new ExpandoObject();
                vm.Id = id;

                availableProductsViewModel[id] = vm;
            }

            return availableProductsViewModel;
        }
    }
}
