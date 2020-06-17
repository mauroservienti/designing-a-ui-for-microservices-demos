using Microsoft.EntityFrameworkCore;
using Warehouse.Api.Data.Models;

namespace Warehouse.Api.Data
{
    public class WarehouseContext : DbContext
    {
        public static void CreateSeedData()
        {
            using var db = new WarehouseContext();
            foreach (var stockItem in Initial.Data())
            {
                db.StockItems.Add(stockItem);
            }

            db.SaveChanges();
        }

        public DbSet<StockItem> StockItems { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase(databaseName: "Warehouse");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<StockItem>();

            base.OnModelCreating(modelBuilder);
        }

        static class Initial
        {
            internal static StockItem[] Data()
            {
                return new[]
                {
                    new StockItem()
                    {
                        Id = 1,
                        ProductId = 1,
                        Inventory = 4,
                    },
                    new StockItem()
                    {
                        Id = 2,
                        ProductId = 2,
                        Inventory = 0,
                    }
                };
            }

        }
    }
}
