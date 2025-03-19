using Microsoft.EntityFrameworkCore;
using SalesApi.Domain.Entities;

namespace SalesApi.Infrastructure.Database
{
    public class SalesDbContext : DbContext
    {
        public SalesDbContext(DbContextOptions<SalesDbContext> options) : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<DiscountConfiguration> DiscountConfigurations { get; set; }
        public DbSet<Sale> Sales { get; set; }
        public DbSet<SaleItem> SaleItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(SalesDbContext).Assembly);

            // Seed DiscountConfigurations
            modelBuilder.Entity<DiscountConfiguration>().HasData(
                new DiscountConfiguration { Id = 1, MinQuantity = 4, MaxQuantity = 9, DiscountPercentage = 5.0m },
                new DiscountConfiguration { Id = 2, MinQuantity = 10, MaxQuantity = 20, DiscountPercentage = 10.0m }
            );
        }
    }
}
