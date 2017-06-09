using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using ExpertSystem.Models;

namespace ExpertSystem
{
    public class ESContext : DbContext
    {
        public ESContext() : base("DBContext") { }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
        }

        public DbSet<Location> Locations { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductRule> ProductRules { get; set; }
        public DbSet<RequireRule> Rules { get; set; }
        public DbSet<Truck> Trucks { get; set; }
        public DbSet<TruckRule> TruckRules { get; set; }
        public DbSet<Warehouse> Warehouses { get; set; }
    }
}