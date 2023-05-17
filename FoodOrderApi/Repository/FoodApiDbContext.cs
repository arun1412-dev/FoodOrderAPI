using FoodOrderApi.Model;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace FoodOrderApi.Repository
{
    public class FoodApiDbContext : DbContext
    {
        public FoodApiDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Restaurant> Restaurants { get; set; }

        public DbSet<Menu> Menus { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<RestaurantWithMenu> RestaurantWithMenus { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}