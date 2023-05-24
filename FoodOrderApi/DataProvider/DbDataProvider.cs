using FoodOrderApi.Model.Domain;
using FoodOrderApi.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FoodOrderApi.DataProvider
{
    public class DbDataProvider : Controller, IDataProvider
    {
        private readonly FoodApiDbContext foodApiDbContext;

        public DbDataProvider(FoodApiDbContext foodApiDbContext)
        {
            this.foodApiDbContext = foodApiDbContext;
        }

        public async Task<IEnumerable<Menu>> GetMenus()
        {
            return foodApiDbContext.Menus.Select(x => x);
        }

        public async Task<IEnumerable<Order>> GetOrderByName(string customerName)
        {
            return foodApiDbContext.Orders.Select(x => x).Where(item => item.CustomerName == customerName);
        }

        public async Task<IEnumerable<Restaurant>> GetRestaurant()
        {
            return await foodApiDbContext.Restaurants.ToListAsync();
        }

        public async Task<IList<string>?> GetRestaurantWithMenu(string restaurantName)
        {
            var getParticularRestaurant = await (foodApiDbContext.RestaurantWithMenus.FirstOrDefaultAsync(item => item.RestaurantName.ToLower() == restaurantName.ToLower()));
            if (getParticularRestaurant != null)
            {
                return getParticularRestaurant.Menus;
            }
            else
            {
                return null;
            }
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
    }
}