using Catalog.Api.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Api.Data
{
    public class MarketingContext : DbContext
    {
        private static string _databaseName = "Catalog";

        internal static void CreateSeedData(string databaseName = null)
        {
            if (!string.IsNullOrWhiteSpace(databaseName))
            {
                _databaseName = databaseName;
            }

            using var db = new MarketingContext();
            foreach (var productDetails in Initial.Data())
            {
                db.ProductsDetails.Add(productDetails);
            }

            db.SaveChanges();
        }

        internal static void DropDatabase()
        {
            using var db = new MarketingContext();
            db.Database.EnsureDeleted();
        }

        public DbSet<ProductDetails> ProductsDetails { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase(databaseName: _databaseName);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProductDetails>();

            base.OnModelCreating(modelBuilder);
        }

        static class Initial
        {
            internal static ProductDetails[] Data()
            {
                return new[]
                {
                    new ProductDetails()
                    {
                        Id = 1,
                        Name = "Banana Holder",
                        Description = "Outdoor travel cute banana protector storage box"
                    },
                    new ProductDetails()
                    {
                        Id = 2,
                        Name = "Nokia Lumia 635",
                        Description = "Amazing phone, unfortunately not understood by market"
                    }
                };
            }

        }
    }
}
