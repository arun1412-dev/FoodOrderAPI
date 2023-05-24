using FoodOrderApi.Model.Domain;
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
            base.OnModelCreating(modelBuilder);
            // Seed data for Restaurants
            var restaurants = new List<Restaurant>()
            {
                    new Restaurant()
                {
                    RestaurantID = 1,
                    RestaurantName = "Kove",
                    RestaurantType = "Fine Dining",
                    RestaurantPhoneNumber = 1234567890,
                    RestaurantLocation = "RS Puram"
                },
                new Restaurant()
                {
                    RestaurantID = 2,
                    RestaurantName = "Annapoorna",
                    RestaurantType = "Casual Dining",
                    RestaurantPhoneNumber = 1234567890,
                    RestaurantLocation = "RS Puram"
                },
                new Restaurant()
                {
                    RestaurantID = 3,
                    RestaurantName = "Chocolate Room",
                    RestaurantType = "Desserts and Beerages",
                    RestaurantPhoneNumber = 1234567890,
                    RestaurantLocation = "Race Course Road"
                },
                new Restaurant()
                {
                    RestaurantID = 4,
                    RestaurantName = "Orbis",
                    RestaurantType = "Multi Cuisine",
                    RestaurantPhoneNumber = 1234567890,
                    RestaurantLocation = "Avinashi Road",
                }
            };
            modelBuilder.Entity<Restaurant>().HasData(restaurants);
            // Seed data for RestaurantsWithMenus
            var restaurantswithmenu = new List<RestaurantWithMenu>()
            {
                new RestaurantWithMenu()
                {
                    RestaurantID = 1,
                    RestaurantName = "Kove",
                    RestaurantType = "Fine Dining",
                    RestaurantPhoneNumber = 1234567890,
                    RestaurantLocation = "RS Puram",

                    Menus = new List<string> { "Thai Broccoli Salad", "Curd Rice Aracini", "Chettinad Cutlet" }
                },
                new RestaurantWithMenu()
                {
                    RestaurantID = 2,
                    RestaurantName = "Annapoorna",
                    RestaurantType = "Casual Dining",
                    RestaurantPhoneNumber = 1234567890,
                    RestaurantLocation = "RS Puram",
                    Menus = new List<string> { "Idly(2)", "Pongal", "Roast" }
                },

                new RestaurantWithMenu()
                {
                    RestaurantID = 3,
                    RestaurantName = "Chocolate Room",
                    RestaurantType = "Desserts and Beerages",
                    RestaurantPhoneNumber = 1234567890,
                    RestaurantLocation = "Race Course Road",
                    Menus = new List<string> { "Chocolate Sizzler", "Choco Brownie Bomb", "Chocolate Cookies" }
                },

                new RestaurantWithMenu()
                {
                    RestaurantID = 4,
                    RestaurantName = "Orbis",
                    RestaurantType = "Multi Cuisine",
                    RestaurantPhoneNumber = 1234567890,
                    RestaurantLocation = "Avinashi Road",
                    Menus = new List<string> { "Vegetable Club Sandwich", "Veg Combo Meal", "Orbis Signature Rice" }
                }
            };
            modelBuilder.Entity<RestaurantWithMenu>().HasData(restaurantswithmenu);
            // Seed data for Menu
            var menu = new List<Menu>()
            {
                new Menu() {Id = 1,  RestaurantID = 1, ProductID = 1, ProductName = "Thai Broccoli Salad", ProductPrice = "255" },
                new Menu() {Id = 2,  RestaurantID = 1, ProductID = 2, ProductName = "Chettinad Cutlet", ProductPrice = "275" },
                new Menu() {Id = 3,  RestaurantID = 1, ProductID = 3, ProductName = "Curd Rice Aracini", ProductPrice = "265" },
                new Menu() {Id = 4,  RestaurantID = 2, ProductID = 1, ProductName = "Idly(2)", ProductPrice = "33" },
                new Menu() {Id = 5,  RestaurantID = 2, ProductID = 2, ProductName = "Pongal", ProductPrice = "48" },
                new Menu() {Id = 6,  RestaurantID = 2, ProductID = 3, ProductName = "Roast", ProductPrice = "75" },
                new Menu() {Id = 7,  RestaurantID = 3, ProductID = 1, ProductName = "Chocolate Sizzler", ProductPrice = "299" },
                new Menu() {Id = 8,  RestaurantID = 3, ProductID = 2, ProductName = "Choco Brownie Bomb", ProductPrice = "249" },
                new Menu() {Id = 9,  RestaurantID = 3, ProductID = 3, ProductName = "Chocolate Cookies", ProductPrice = "199" },
                new Menu() {Id = 10,  RestaurantID = 4, ProductID = 1, ProductName = "Vegetable Club Sandwich", ProductPrice = "150" },
                new Menu() {Id = 11,  RestaurantID = 4, ProductID = 2, ProductName = "Veg Combo Meal", ProductPrice = "439" },
                new Menu() {Id = 12,  RestaurantID = 4, ProductID = 3, ProductName = "Orbis Signature Rice", ProductPrice = "250" }
            };
            modelBuilder.Entity<Menu>().HasData(menu);
        }
    }
}