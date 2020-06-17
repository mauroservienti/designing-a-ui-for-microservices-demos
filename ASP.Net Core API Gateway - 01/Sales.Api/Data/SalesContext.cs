using Microsoft.EntityFrameworkCore;
using Sales.Api.Data.Models;

namespace Sales.Api.Data
{
    public class SalesContext : DbContext
    {
        public static void CreateSeedData()
        {
            using (var db = new SalesContext())
            {
                foreach (var productPrice in Initial.Data())
                {
                    db.ProductsPrices.Add(productPrice);
                }

                db.SaveChanges();
            }
        }

        public DbSet<ProductPrice> ProductsPrices { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase(databaseName: "Sales");
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
