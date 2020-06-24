using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using ApprovalTests;
using ApprovalTests.Reporters;
using JsonUtils;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using ServiceComposer.AspNetCore.Testing;
using Xunit;

namespace WebApp.Tests
{
    public class When_Invoking_home_page: IDisposable
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

        public When_Invoking_home_page()
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
        public async Task Should_render_available_products()
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
            var webAppClient = webApp.CreateClient();

            // Act
            var homeResponse = await webAppClient.GetAsync("/");
            var result = await homeResponse.Content.ReadAsStringAsync();

            // Assert
            Assert.True(homeResponse.IsSuccessStatusCode);
            Approvals.Verify(result);
        }
    }
}