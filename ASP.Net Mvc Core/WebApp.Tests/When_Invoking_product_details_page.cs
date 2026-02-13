using System;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using ApprovalTests;
using ApprovalTests.Reporters;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using ServiceComposer.AspNetCore.Testing;
using Xunit;

namespace WebApp.Tests
{
    [Collection("Sequential")]
    public class When_Invoking_product_details_page: IDisposable
    {
        readonly HttpClient _warehouseApiClient;
        readonly HttpClient _shippingApiClient;
        readonly HttpClient _salesApiClient;
        readonly HttpClient _catalogApiClient;

        void IDisposable.Dispose()
        {
            Warehouse.Api.Data.WarehouseContext.DropDatabase();
            Shipping.Api.Data.ShippingContext.DropDatabase();
            Sales.Api.Data.SalesContext.DropDatabase();
            Catalog.Api.Data.MarketingContext.DropDatabase();
        }

        public When_Invoking_product_details_page()
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
        [UseReporter(typeof(DiffReporter))]
        [MethodImpl(MethodImplOptions.NoInlining)]
        public async Task Should_render_expected_product()
        {
            // Arrange
            var webApp = new WebApplicationFactoryWithWebHost<WebApp.Startup>
            {
                BuilderCustomization = builder =>
                {
                    HttpClient ClientProvider(string name) =>
                        name switch
                        {
                            var val when val == typeof(Shipping.ViewModelComposition.ProductDetailsGetHandler).FullName => _shippingApiClient,
                            var val when val == typeof(Warehouse.ViewModelComposition.ProductDetailsGetHandler).FullName => _warehouseApiClient,
                            var val when val == typeof(Warehouse.ViewModelComposition.AvailableProductsLoadedSubscriber).FullName => _warehouseApiClient,
                            var val when val == typeof(Sales.ViewModelComposition.ProductDetailsGetHandler).FullName => _salesApiClient,
                            var val when val == typeof(Sales.ViewModelComposition.AvailableProductsLoadedSubscriber).FullName => _salesApiClient,
                            var val when val == typeof(Catalog.ViewModelComposition.ProductDetailsGetHandler).FullName => _catalogApiClient,
                            var val when val == typeof(Catalog.ViewModelComposition.CompositionHandlers.AvailableProductsCompositionHandler).FullName => _catalogApiClient,
                            var val when val == typeof(Catalog.ViewModelComposition.AvailableProductsLoadedSubscriber).FullName => _catalogApiClient,
                            _ => throw new NotSupportedException($"Missing HTTP client for {name}")
                        };

                    builder.ConfigureServices(services =>
                    {
                        services.Replace(new ServiceDescriptor(typeof(IHttpClientFactory), new DelegateHttpClientFactory(ClientProvider)));
                    });
                }
            };
            var webAppClient = webApp.CreateClient();

            // Act
            var detailsResponse = await webAppClient.GetAsync("/products/details/1");
            var result = await detailsResponse.Content.ReadAsStringAsync();

            // Assert
            Assert.True(detailsResponse.IsSuccessStatusCode);
            Approvals.Verify(result, received =>
            {
                var begin = received.LastIndexOf("site.js?v=") + 10;
                var end = received.LastIndexOf("\"></script>");

                return $"{received.Substring(0,begin)}|version-scrubbed|{received.Substring(end)}";
            });
        }
    }
}