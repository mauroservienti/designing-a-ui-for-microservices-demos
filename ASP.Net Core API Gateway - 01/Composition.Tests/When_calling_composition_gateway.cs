using System;
using System.Net.Http;
using System.Threading.Tasks;
using JsonUtils;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Xunit;

namespace Composition.Tests
{
    public class When_calling_composition_gateway : IDisposable
    {
        readonly HttpClient _warehouseApiClient;
        readonly HttpClient _shippingApiClient;
        readonly HttpClient _salesApiClient;
        private HttpClient _catalogApiClient;

        void IDisposable.Dispose()
        {
            Warehouse.Api.Data.WarehouseContext.DropDatabase();
            Shipping.Api.Data.ShippingContext.DropDatabase();
            Sales.Api.Data.SalesContext.DropDatabase();
            Catalog.Api.Data.MarketingContext.DropDatabase();
        }

        public When_calling_composition_gateway()
        {
            Warehouse.Api.Data.WarehouseContext.CreateSeedData(Guid.NewGuid().ToString());
            var warehouseApi = new WebApplicationFactoryWithWebHost<Warehouse.Api.Startup>();
            _warehouseApiClient = warehouseApi.CreateClient();

            Shipping.Api.Data.ShippingContext.CreateSeedData(Guid.NewGuid().ToString());
            var shippingApi = new WebApplicationFactoryWithWebHost<Shipping.Api.Startup>();
            _shippingApiClient = shippingApi.CreateClient();

            Sales.Api.Data.SalesContext.CreateSeedData(Guid.NewGuid().ToString());
            var salesApi = new WebApplicationFactoryWithWebHost<Sales.Api.Startup>();
            _salesApiClient = salesApi.CreateClient();

            Catalog.Api.Data.MarketingContext.CreateSeedData(Guid.NewGuid().ToString());
            var catalogApi = new WebApplicationFactoryWithWebHost<Catalog.Api.Startup>();
            _catalogApiClient = catalogApi.CreateClient();
        }

        [Fact]
        public async Task Get_composed_product_should_return_expected_values()
        {
            // Arrange
            var compositionGateway = new WebApplicationFactoryWithWebHost<CompositionGateway.Startup>
            {
                BuilderCustomization = builder =>
                {
                    HttpClient ClientProvider(string name) =>
                        name switch
                        {
                            var val when val == typeof(Shipping.ViewModelComposition.ProductDetailsGetHandler).FullName => _shippingApiClient,
                            var val when val == typeof(Warehouse.ViewModelComposition.ProductDetailsGetHandler).FullName => _warehouseApiClient,
                            var val when val == typeof(Sales.ViewModelComposition.ProductDetailsGetHandler).FullName => _salesApiClient,
                            var val when val == typeof(Catalog.ViewModelComposition.ProductDetailsGetHandler).FullName => _catalogApiClient,
                            _ => throw new NotSupportedException($"Missing HTTP client for {name}")
                        };

                    builder.ConfigureServices(services =>
                    {
                        services.Replace(new ServiceDescriptor(typeof(IHttpClientFactory), new DelegateHttpClientFactory(ClientProvider)));
                    });
                }
            };
            var compositionClient = compositionGateway.CreateClient();
            compositionClient.DefaultRequestHeaders.Add("Accept-Casing", "casing/pascal");

            // Act
            var composedResponse = await compositionClient.GetAsync("/products/details/1");
            dynamic composedViewModel = await composedResponse.Content.AsExpando();

            // Assert
            Assert.True(composedResponse.IsSuccessStatusCode);

            Assert.Equal("Banana Holder", composedViewModel.ProductName);
            Assert.Equal("Outdoor travel cute banana protector storage box", composedViewModel.ProductDescription);
            Assert.Equal(10, composedViewModel.ProductPrice);
            Assert.Equal(4, composedViewModel.ProductInventory);
            Assert.Equal(false, composedViewModel.ProductOutOfStock);
            Assert.Equal("Express Delivery, Regular mail", composedViewModel.ProductShippingOptions);
        }
    }

    public class DelegateHttpClientFactory : IHttpClientFactory
    {
        private readonly Func<string, HttpClient> _httpClientProvider;

        public DelegateHttpClientFactory(Func<string, HttpClient> httpClientProvider)
        {
            _httpClientProvider = httpClientProvider;
        }

        public HttpClient CreateClient(string name)
        {
            return _httpClientProvider(name);
        }
    }
}