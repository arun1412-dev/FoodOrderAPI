using FoodOrderApi.Model;
using FoodOrderApi.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FoodOrderApi.DataProvider
{
    public class DbDataProvider : Controller, IDataProvider
    {
        private readonly FoodApiDbContext foodApiDbContext;
        public static List<Order> _CustomerOrder = new List<Order>();

        public DbDataProvider(FoodApiDbContext foodApiDbContext)
        {
            this.foodApiDbContext = foodApiDbContext;
            //AddRestarunt();
            //AddRestrauntData();
            //AddRestaruntWithMenu();
        }

        public IEnumerable<Menu> GetMenus()
        {
            return foodApiDbContext.Menus.Select(x => x);
        }

        public IEnumerable<Order> GetOrderByName(string customerName)
        {
            return foodApiDbContext.Orders.Select(x => x).Where(item => item.CustomerName == customerName);
        }

        public IEnumerable<Restaurant> GetRestaurant()
        {
            return foodApiDbContext.Restaurants.Select(x => x);
        }

        public IEnumerable<IList<string>> GetRestaurantWithMenu(string restaurantName)
        {
            return foodApiDbContext.RestaurantWithMenus.Select(x => x).Where(item => item.RestaurantName.ToLower() == restaurantName.ToLower()).Select(x => x.Menus);
        }

        public void PlaceOrder(Order newCustomerOrder)
        {
            foodApiDbContext.Orders.Add(newCustomerOrder);
            foodApiDbContext.SaveChanges();
        }

        public void DeleteOrder(Order newCustomerOrder)
        {
            foodApiDbContext.Orders.Remove(newCustomerOrder);
            foodApiDbContext.SaveChanges();
        }

        public void AddRestarunt()
        {
            foodApiDbContext.Restaurants.Add(new Restaurant()
            {
                RestaurantID = 1,
                RestaurantName = "Kove",
                RestaurantType = "Fine Dining",
                RestaurantPhoneNumber = 1234567890,
                RestaurantLocation = "RS Puram"
            });
            foodApiDbContext.Restaurants.Add(new Restaurant()
            {
                RestaurantID = 2,
                RestaurantName = "Annapoorna",
                RestaurantType = "Casual Dining",
                RestaurantPhoneNumber = 1234567890,
                RestaurantLocation = "RS Puram"
            });
            foodApiDbContext.Restaurants.Add(new Restaurant()
            {
                RestaurantID = 3,
                RestaurantName = "Chocolate Room",
                RestaurantType = "Desserts and Beerages",
                RestaurantPhoneNumber = 1234567890,
                RestaurantLocation = "Race Course Road"
            });
            foodApiDbContext.Restaurants.Add(new Restaurant()
            {
                RestaurantID = 4,
                RestaurantName = "Orbis",
                RestaurantType = "Multi Cuisine",
                RestaurantPhoneNumber = 1234567890,
                RestaurantLocation = "Avinashi Road",
            });
        }

        public void AddRestaruntWithMenu()
        {
            foodApiDbContext.RestaurantWithMenus.Add(new RestaurantWithMenu()
            {
                RestaurantID = 1,
                RestaurantName = "Kove",
                RestaurantType = "Fine Dining",
                RestaurantPhoneNumber = 1234567890,
                RestaurantLocation = "RS Puram",

                Menus = new List<string> { "Thai Broccoli Salad", "Curd Rice Aracini", "Chettinad Cutlet" }
            });
            foodApiDbContext.RestaurantWithMenus.Add(new RestaurantWithMenu()
            {
                RestaurantID = 2,
                RestaurantName = "Annapoorna",
                RestaurantType = "Casual Dining",
                RestaurantPhoneNumber = 1234567890,
                RestaurantLocation = "RS Puram",
                Menus = new List<string> { "Idly(2)", "Pongal", "Roast" }
            });

            foodApiDbContext.RestaurantWithMenus.Add(new RestaurantWithMenu()
            {
                RestaurantID = 3,
                RestaurantName = "Chocolate Room",
                RestaurantType = "Desserts and Beerages",
                RestaurantPhoneNumber = 1234567890,
                RestaurantLocation = "Race Course Road",
                Menus = new List<string> { "Chocolate Sizzler", "Choco Brownie Bomb", "Chocolate Cookies" }
            });

            foodApiDbContext.RestaurantWithMenus.Add(new RestaurantWithMenu()
            {
                RestaurantID = 4,
                RestaurantName = "Orbis",
                RestaurantType = "Multi Cuisine",
                RestaurantPhoneNumber = 1234567890,
                RestaurantLocation = "Avinashi Road",
                Menus = new List<string> { "Vegetable Club Sandwich", "Veg Combo Meal", "Orbis Signature Rice" }
            });

            foodApiDbContext.SaveChanges();
        }

        public void AddRestrauntData()
        {
            foodApiDbContext.Menus.Add(new Menu() { RestaurantID = 1, ProductID = 1, ProductName = "Thai Broccoli Salad", ProductPrice = "255" });
            foodApiDbContext.Menus.Add(new Menu() { RestaurantID = 1, ProductID = 2, ProductName = "Chettinad Cutlet", ProductPrice = "275" });
            foodApiDbContext.Menus.Add(new Menu() { RestaurantID = 1, ProductID = 3, ProductName = "Curd Rice Aracini", ProductPrice = "265" });
            foodApiDbContext.Menus.Add(new Menu() { RestaurantID = 2, ProductID = 1, ProductName = "Idly(2)", ProductPrice = "33" });
            foodApiDbContext.Menus.Add(new Menu() { RestaurantID = 2, ProductID = 2, ProductName = "Pongal", ProductPrice = "48" });
            foodApiDbContext.Menus.Add(new Menu() { RestaurantID = 2, ProductID = 3, ProductName = "Roast", ProductPrice = "75" });
            foodApiDbContext.Menus.Add(new Menu() { RestaurantID = 3, ProductID = 1, ProductName = "Chocolate Sizzler", ProductPrice = "299" });
            foodApiDbContext.Menus.Add(new Menu() { RestaurantID = 3, ProductID = 2, ProductName = "Choco Brownie Bomb", ProductPrice = "249" });
            foodApiDbContext.Menus.Add(new Menu() { RestaurantID = 3, ProductID = 3, ProductName = "Chocolate Cookies", ProductPrice = "199" });
            foodApiDbContext.Menus.Add(new Menu() { RestaurantID = 4, ProductID = 1, ProductName = "Vegetable Club Sandwich", ProductPrice = "150" });
            foodApiDbContext.Menus.Add(new Menu() { RestaurantID = 4, ProductID = 2, ProductName = "Veg Combo Meal", ProductPrice = "439" });
            foodApiDbContext.Menus.Add(new Menu() { RestaurantID = 4, ProductID = 3, ProductName = "Orbis Signature Rice", ProductPrice = "250" });

            foodApiDbContext.SaveChanges();
        }
    }
}