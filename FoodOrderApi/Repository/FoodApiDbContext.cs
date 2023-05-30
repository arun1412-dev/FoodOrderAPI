using FoodOrderApi.Model.Domain;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
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
                    RestaurantID = Guid.Parse("BAAD586A-ACCF-4433-98F0-2F861E683354"),
                    RestaurantName = "Kove",
                    RestaurantType = "Fine Dining",
                    RestaurantPhoneNumber = 1234567890,
                    RestaurantLocation = "RS Puram"
                },
                new Restaurant()
                {
                    RestaurantID =  Guid.Parse("7E83A461-5A28-4B11-83A5-31458449B7AC"),
                    RestaurantName = "Annapoorna",
                    RestaurantType = "Casual Dining",
                    RestaurantPhoneNumber = 1234567890,
                    RestaurantLocation = "RS Puram"
                },
                new Restaurant()
                {
                    RestaurantID =  Guid.Parse("7A03CA50-719F-48E7-AE0E-E4E299F3112B"),
                    RestaurantName = "Chocolate Room",
                    RestaurantType = "Desserts and Beerages",
                    RestaurantPhoneNumber = 1234567890,
                    RestaurantLocation = "Race Course Road"
                },
                new Restaurant()
                {
                    RestaurantID =  Guid.Parse("2818B184-94BE-4A91-A3B8-A510B00BD6F5"),
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
                    RestaurantID =  Guid.Parse("BAAD586A-ACCF-4433-98F0-2F861E683354"),
                    RestaurantName = "Kove",
                    RestaurantType = "Fine Dining",
                    RestaurantPhoneNumber = 1234567890,
                    RestaurantLocation = "RS Puram",

                    Menus = new List<string> { "Thai Broccoli Salad", "Curd Rice Aracini", "Chettinad Cutlet" }
                },
                new RestaurantWithMenu()
                {
                    RestaurantID =  Guid.Parse("7E83A461-5A28-4B11-83A5-31458449B7AC"),
                    RestaurantName = "Annapoorna",
                    RestaurantType = "Casual Dining",
                    RestaurantPhoneNumber = 1234567890,
                    RestaurantLocation = "RS Puram",
                    Menus = new List<string> { "Idly(2)", "Pongal", "Roast" }
                },

                new RestaurantWithMenu()
                {
                    RestaurantID = Guid.Parse("7A03CA50-719F-48E7-AE0E-E4E299F3112B"),
                    RestaurantName = "Chocolate Room",
                    RestaurantType = "Desserts and Beerages",
                    RestaurantPhoneNumber = 1234567890,
                    RestaurantLocation = "Race Course Road",
                    Menus = new List<string> { "Chocolate Sizzler", "Choco Brownie Bomb", "Chocolate Cookies" }
                },

                new RestaurantWithMenu()
                {
                    RestaurantID = Guid.Parse("2818B184-94BE-4A91-A3B8-A510B00BD6F5"),
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
                new Menu() {RestaurantID = Guid.Parse("BAAD586A-ACCF-4433-98F0-2F861E683354"), ProductID = Guid.Parse("E0194510-6AD1-4AC2-BF31-E572CAA09BA1"), ProductName = "Thai Broccoli Salad", ProductPrice = "255" },
                new Menu() {RestaurantID = Guid.Parse("BAAD586A-ACCF-4433-98F0-2F861E683354"), ProductID = Guid.Parse("F2612C12-B189-4D23-9532-E7C07A1EEBE3"), ProductName = "Chettinad Cutlet", ProductPrice = "275" },
                new Menu() {RestaurantID = Guid.Parse("BAAD586A-ACCF-4433-98F0-2F861E683354"), ProductID = Guid.Parse("F613A27F-93C2-4028-9DEB-DDC4B8B45E3A"), ProductName = "Curd Rice Aracini", ProductPrice = "265" },
                new Menu() {RestaurantID = Guid.Parse("7E83A461-5A28-4B11-83A5-31458449B7AC"), ProductID = Guid.Parse("BE5A857B-1336-4D13-A1D8-B0C9683689CA"), ProductName = "Idly(2)", ProductPrice = "33" },
                new Menu() {RestaurantID = Guid.Parse("7E83A461-5A28-4B11-83A5-31458449B7AC"), ProductID = Guid.Parse("780717AD-8473-4C23-B9B0-A256FD80F13F"), ProductName = "Pongal", ProductPrice = "48" },
                new Menu() {RestaurantID = Guid.Parse("7E83A461-5A28-4B11-83A5-31458449B7AC"), ProductID = Guid.Parse("E1764DA4-941D-463A-8531-43B701306780"), ProductName = "Roast", ProductPrice = "75" },
                new Menu() {RestaurantID = Guid.Parse("7A03CA50-719F-48E7-AE0E-E4E299F3112B"), ProductID = Guid.Parse("1FF36C1A-3E10-4651-88E4-7FC4736BF4D8"), ProductName = "Chocolate Sizzler", ProductPrice = "299" },
                new Menu() {RestaurantID = Guid.Parse("7A03CA50-719F-48E7-AE0E-E4E299F3112B"), ProductID = Guid.Parse("316DC710-146C-430B-9B0F-E9AA51B1E8E9"), ProductName = "Choco Brownie Bomb", ProductPrice = "249" },
                new Menu() {RestaurantID = Guid.Parse("7A03CA50-719F-48E7-AE0E-E4E299F3112B"), ProductID = Guid.Parse("3A011230-DB41-4407-9218-347F51391818"), ProductName = "Chocolate Cookies", ProductPrice = "199" },
                new Menu() {RestaurantID = Guid.Parse("2818B184-94BE-4A91-A3B8-A510B00BD6F5"), ProductID = Guid.Parse("C8E03D8C-3438-4583-B12E-1BA54C3670D6"), ProductName = "Vegetable Club Sandwich", ProductPrice = "150" },
                new Menu() {RestaurantID = Guid.Parse("2818B184-94BE-4A91-A3B8-A510B00BD6F5"), ProductID = Guid.Parse("A91BE131-8704-45AE-9BA7-60E70C58ABB4"), ProductName = "Veg Combo Meal", ProductPrice = "439" },
                new Menu() {RestaurantID = Guid.Parse("2818B184-94BE-4A91-A3B8-A510B00BD6F5"), ProductID = Guid.Parse("5849B06F-9731-4E31-A908-6E7E9DAFDD5A"), ProductName = "Orbis Signature Rice", ProductPrice = "250" }
            };
            modelBuilder.Entity<Menu>().HasData(menu);
        }
    }
}