using FoodOrderApi.Model;
using Microsoft.AspNetCore.Mvc;

namespace FoodOrderApi.DataProvider
{
    public interface IDataProvider
    {
        IEnumerable<Menu> GetMenus();

        IEnumerable<Restaurant> GetRestaurant();

        IEnumerable<IList<string>> GetRestaurantWithMenu(string restaurantName);

        IEnumerable<Order> GetOrderByName(string customerName);

        void PlaceOrder(Order newCustomerOrder);

        void DeleteOrder(Order newCustomerOrder);
    }
}