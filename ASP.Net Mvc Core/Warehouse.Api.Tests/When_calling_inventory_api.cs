using System;
using System.Threading.Tasks;
using JsonUtils;
using ServiceComposer.AspNetCore.Testing;
using Warehouse.Api.Data.Models;
using Xunit;

namespace Warehouse.Api.Tests
{
    public class When_calling_inventory_api : IDisposable
    {
        public When_calling_inventory_api()
        {
            Data.WarehouseContext.CreateSeedData(Guid.NewGuid().ToString());
        }

        void IDisposable.Dispose()
        {
            Data.WarehouseContext.DropDatabase();
        }

        [Fact]
        public async Task Get_product_should_return_200()
        {
            // Arrange
            var client = new WebApplicationFactoryWithWebHost<Startup>().CreateClient();

            // Act
            var response = await client.GetAsync("/api/inventory/product/1");

            // Assert
            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task Get_product_should_return_expected_StockItem()
        {
            // Arrange
            var expectedStockItem = new StockItem()
            {
                Id = 1,
                ProductId = 1,
                Inventory = 4,
            };
            var client = new WebApplicationFactoryWithWebHost<Startup>().CreateClient();

            // Act
            var response = await client.GetAsync("/api/inventory/product/1");
            var stockItem = await response.Content.As<StockItem>();

            // Assert
            Assert.Equal(expectedStockItem.Id, stockItem.Id);
            Assert.Equal(expectedStockItem.Inventory, stockItem.Inventory);
            Assert.Equal(expectedStockItem.ProductId, stockItem.ProductId);
        }

        [Fact]
        public async Task Get_products_by_ids_returns_expected_values()
        {
            // Arrange
            var client = new WebApplicationFactoryWithWebHost<Startup>().CreateClient();

            // Act
            var response = await client.GetAsync("/api/inventory/products/1,2");
            var stockItems = await response.Content.As<StockItem[]>();

            // Assert
            Assert.Collection(stockItems,
                item =>
                {
                    Assert.Equal(1, item.Id);
                    Assert.Equal(1, item.ProductId);
                    Assert.Equal(4, item.Inventory);
                },
                item =>
                {
                    Assert.Equal(2, item.Id);
                    Assert.Equal(2, item.ProductId);
                    Assert.Equal(0, item.Inventory);
                });
        }
    }
}