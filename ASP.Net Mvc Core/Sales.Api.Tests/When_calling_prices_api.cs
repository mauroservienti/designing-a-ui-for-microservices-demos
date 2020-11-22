using System;
using System.Threading.Tasks;
using JsonUtils;
using ServiceComposer.AspNetCore.Testing;
using Sales.Api.Data.Models;
using Xunit;

namespace Sales.Api.Tests
{
    public class When_calling_prices_api : IDisposable
    {
        public When_calling_prices_api()
        {
            Data.SalesContext.CreateSeedData(Guid.NewGuid().ToString());
        }

        void IDisposable.Dispose()
        {
            Data.SalesContext.DropDatabase();
        }

        [Fact]
        public async Task Get_product_should_return_200()
        {
            // Arrange
            using var client = new WebApplicationFactoryWithWebHost<Startup>().CreateClient();

            // Act
            var response = await client.GetAsync("/api/prices/product/1");

            // Assert
            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task Get_product_should_return_expected_ProductPrice()
        {
            // Arrange
            var expectedProductPrice = new ProductPrice()
            {
                Id = 1,
                Price = 10.00m
            };
            var client = new WebApplicationFactoryWithWebHost<Startup>().CreateClient();

            // Act
            var response = await client.GetAsync("/api/prices/product/1");
            var productPrice = await response.Content.As<ProductPrice>();

            // Assert
            Assert.Equal(expectedProductPrice.Id, productPrice.Id);
            Assert.Equal(expectedProductPrice.Price, productPrice.Price);
        }

        [Fact]
        public async Task Get_products_by_ids_returns_expected_values()
        {
            // Arrange
            var client = new WebApplicationFactoryWithWebHost<Startup>().CreateClient();

            // Act
            var response = await client.GetAsync("/api/prices/products/1,2");
            var productPrices = await response.Content.As<ProductPrice[]>();

            // Assert
            Assert.Collection(productPrices,
                item =>
                {
                    Assert.Equal(1, item.Id);
                    Assert.Equal(10.00m, item.Price);
                },
                item =>
                {
                    Assert.Equal(2, item.Id);
                    Assert.Equal(100.00m, item.Price);
                });
        }
    }
}