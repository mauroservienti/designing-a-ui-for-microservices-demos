using System;
using System.Threading.Tasks;
using JsonUtils;
using ServiceComposer.AspNetCore.Testing;
using Shipping.Api.Data.Models;
using Xunit;

namespace Shipping.Api.Tests
{
    public class Wen_calling_shipping_options_api : IDisposable
    {
        public Wen_calling_shipping_options_api()
        {
            Data.ShippingContext.CreateSeedData(Guid.NewGuid().ToString());
        }

        void IDisposable.Dispose()
        {
            Data.ShippingContext.DropDatabase();
        }

        [Fact]
        public async Task Get_product_should_return_200()
        {
            // Arrange
            var client = new WebApplicationFactoryWithWebHost<Startup>().CreateClient();

            // Act
            var response = await client.GetAsync("/api/shipping-options/product/1");

            // Assert
            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task Get_product_should_return_expected_StockItem()
        {
            // Arrange
            var expectedProductShippingOptions = new ProductShippingOptions()
            {
                Id = 1,
                ProductId = 1,
            };
            expectedProductShippingOptions.Options.Add(new ShippingOption()
            {
                Id = 1,
                ProductShippingOptionsId = 1,
                Option = "Express Delivery",
                EstimatedMinDeliveryDays = 1,
                EstimatedMaxDeliveryDays = 3,
            });
            expectedProductShippingOptions.Options.Add(new ShippingOption()
            {
                Id = 2,
                ProductShippingOptionsId = 1,
                Option = "Regular mail",
                EstimatedMinDeliveryDays = 4,
                EstimatedMaxDeliveryDays = 12,
            });

            var client = new WebApplicationFactoryWithWebHost<Startup>().CreateClient();

            // Act
            var response = await client.GetAsync("/api/shipping-options/product/1");
            var shippingOption = await response.Content.As<ProductShippingOptions>();

            // Assert
            Assert.Equal(expectedProductShippingOptions.Id, shippingOption.Id);
            Assert.Equal(expectedProductShippingOptions.ProductId, shippingOption.ProductId);
            Assert.Equal(expectedProductShippingOptions.Options.Count, shippingOption.Options.Count);
        }
    }
}