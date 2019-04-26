using Microsoft.EntityFrameworkCore;
using Shipping.Data.Models;
using System.Linq;

namespace Shipping.Data
{
    public class ShippingContext : DbContext
    {
        public static void CreateSeedData()
        {
            using (var db = new ShippingContext())
            {
                var options = Initial.ShippingOptions();

                foreach (var productShippingOptions in ShippingContext.Initial.ProductShippingOptions())
                {
                    var optionsForThisProduct = options.Where(o => o.ProductShippingOptionsId == productShippingOptions.Id);
                    productShippingOptions.Options.AddRange(optionsForThisProduct);
                    db.ProductShippingOptions.Add(productShippingOptions);
                }

                db.SaveChanges();
            }
        }

        public DbSet<ProductShippingOptions> ProductShippingOptions { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase(databaseName: "Shipping");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var productShippingOptionsEntity = modelBuilder.Entity<ProductShippingOptions>();
            var shippingOptionsEntity = modelBuilder.Entity<ShippingOption>();

            shippingOptionsEntity
                .HasOne<ProductShippingOptions>()
                .WithMany(pso => pso.Options)
                .IsRequired()
                .HasForeignKey(so => so.ProductShippingOptionsId)
                .OnDelete(DeleteBehavior.Cascade);

            base.OnModelCreating(modelBuilder);
        }

        internal static class Initial
        {
            internal static ShippingOption[] ShippingOptions()
            {
                return new[]
                {
                    new ShippingOption()
                    {
                        Id = 1,
                        ProductShippingOptionsId = 1,
                        Option = "Express Delivery",
                        EstimatedMinDeliveryDays = 1,
                        EstimatedMaxDeliveryDays = 3,
                    },
                    new ShippingOption()
                    {
                        Id = 2,
                        ProductShippingOptionsId = 1,
                        Option = "Regular mail",
                        EstimatedMinDeliveryDays = 4,
                        EstimatedMaxDeliveryDays = 12,
                    },
                    new ShippingOption()
                    {
                        Id = 3,
                        ProductShippingOptionsId = 2,
                        Option = "Fantasy Delivery",
                        EstimatedMinDeliveryDays = int.MaxValue,
                        EstimatedMaxDeliveryDays = int.MaxValue,
                    },
                };
            }

            internal static ProductShippingOptions[] ProductShippingOptions()
            {
                return new[]
                {
                    new ProductShippingOptions()
                    {
                        Id = 1,
                        ProductId = 1,
                    },
                    new ProductShippingOptions()
                    {
                        Id = 2,
                        ProductId = 2,
                    }
                };
            }

        }
    }
}