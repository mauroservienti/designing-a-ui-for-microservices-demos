using Microsoft.EntityFrameworkCore;
using Sales.Api.Data.Models;

namespace Sales.Api.Data
{
    public class SalesContext : DbContext
    {
        private static string _databaseName = "Sales";

        internal static void CreateSeedData(string databaseName = null)
        {
            if (!string.IsNullOrWhiteSpace(databaseName))
            {
                _databaseName = databaseName;
            }

            using var db = new SalesContext();
            foreach (var productPrice in Initial.Data())
            {
                db.ProductsPrices.Add(productPrice);
            }

            db.SaveChanges();
        }

        internal static void DropDatabase()
        {
            using var db = new SalesContext();
            db.Database.EnsureDeleted();
        }

        public DbSet<ProductPrice> ProductsPrices { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase(databaseName: _databaseName);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProductPrice>();

            base.OnModelCreating(modelBuilder);
        }

        internal static class Initial
        {
            internal static ProductPrice[] Data()
            {
                return new[]
                {
                    new ProductPrice()
                    {
                        Id = 1,
                        Price = 10.00m
                    },
                    new ProductPrice()
                    {
                        Id = 2,
                        Price = 100.00m,
                    }
                };
            }
        }
    }
}