using FoodOrderApi.Model;
using Microsoft.AspNetCore.Mvc;

namespace FoodOrderApi.DataProvider
{
    public interface IDataProvider
    {
        IEnumerable<Menu> GetMenuDisplay();

        IEnumerable<Restaurant> GetRestaurant();

        IEnumerable<Order> GetOrderByName(string customerName);

        void PlaceOrder(Order newCustomerOrder);

        void DeleteOrder(Order newCustomerOrder);
    }
}