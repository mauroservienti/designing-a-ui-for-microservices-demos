using Catalog.Api.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Api.Data
{
    public class MarketingContext : DbContext
    {
        public static void CreateSeedData()
        {
            using (var db = new MarketingContext())
            {
                foreach (var productDetails in Initial.Data())
                {
                    db.ProductsDetails.Add(productDetails);
                }

                db.SaveChanges();
            }
        }

        public DbSet<ProductDetails> ProductsDetails { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase(databaseName: "Catalog");
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
