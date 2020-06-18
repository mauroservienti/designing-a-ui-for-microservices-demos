using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Xunit;

namespace Composition.Tests
{
    public class When_calling_composition_gateway : IDisposable
    {
        WebApplicationFactoryWithWebHost<Warehouse.Api.Startup> _warehouseApi;
        WebApplicationFactoryWithWebHost<CompositionGateway.Startup> _compositionGateway;
        HttpClient _warehouseApiClient;

        void IDisposable.Dispose()
        {
            Warehouse.Api.Data.WarehouseContext.DropDatabase();
        }

        public When_calling_composition_gateway()
        {
            _compositionGateway = new WebApplicationFactoryWithWebHost<CompositionGateway.Startup>();
            _warehouseApi = new WebApplicationFactoryWithWebHost<Warehouse.Api.Startup>();
            _warehouseApiClient = _warehouseApi.CreateClient();

            Warehouse.Api.Data.WarehouseContext.CreateSeedData(Guid.NewGuid().ToString());
        }

        [Fact]
        public async Task Get_composed_product_should_return_expected_values()
        {
            // Arrange
            _compositionGateway.BuilderCustomization = builder =>
            {
                builder.ConfigureServices(services =>
                {
                    services.Replace(new ServiceDescriptor(typeof(IHttpClientFactory), new TestHttpClientFactory(_warehouseApiClient)));
                });
            };
            var compositionClient = _compositionGateway.CreateClient();

            // Act
            var composedResponse = await compositionClient.GetAsync("/products/details/1");

            Assert.True(composedResponse.IsSuccessStatusCode);
        }
    }

    public class TestHttpClientFactory : IHttpClientFactory
    {
        private readonly HttpClient _httpClient;

        public TestHttpClientFactory(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public HttpClient CreateClient(string name)
        {
            return _httpClient;
        }
    }
}