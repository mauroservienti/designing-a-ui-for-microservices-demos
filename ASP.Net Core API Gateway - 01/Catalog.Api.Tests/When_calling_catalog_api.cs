using System;
using System.Threading.Tasks;
using Catalog.Api;
using Catalog.Api.Data;
using ServiceComposer.AspNetCore.Testing;
using Catalog.Api.Data.Models;
using JsonUtils;
using Xunit;

namespace Sales.Api.Tests
{
    public class When_calling_catalog_api : IDisposable
    {
        public When_calling_catalog_api()
        {
            MarketingContext.CreateSeedData(Guid.NewGuid().ToString());
        }

        void IDisposable.Dispose()
        {
            MarketingContext.DropDatabase();
        }

        [Fact]
        public async Task Get_product_should_return_200()
        {
            // Arrange
            var client = new WebApplicationFactoryWithWebHost<Startup>().CreateClient();

            // Act
            var response = await client.GetAsync("/api/product-details/product/1");

            // Assert
            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task Get_product_should_return_expected_ProductDetails()
        {
            // Arrange
            var expectedProductDetails = new ProductDetails()
            {
                Id = 1,
                Name = "Banana Holder",
                Description = "Outdoor travel cute banana protector storage box"
            };
            var client = new WebApplicationFactoryWithWebHost<Startup>().CreateClient();

            // Act
            var response = await client.GetAsync("/api/product-details/product/1");
            var productDetails = await response.Content.As<ProductDetails>();

            // Assert
            Assert.Equal(expectedProductDetails.Id, productDetails.Id);
            Assert.Equal(expectedProductDetails.Name, productDetails.Name);
            Assert.Equal(expectedProductDetails.Description, productDetails.Description);
        }
    }
}