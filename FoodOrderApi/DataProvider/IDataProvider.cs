using FoodOrderApi.Model.Domain;

namespace FoodOrderApi.DataProvider
{
    public interface IDataProvider
    {
        Task<IEnumerable<Menu>> GetMenus();

        Task<IEnumerable<Restaurant>> GetRestaurant();

        Task<IList<string>?> GetRestaurantWithMenu(string restaurantName);

        Task<IEnumerable<Order>> GetOrderByName(string customerName);

        void PlaceOrder(Order newCustomerOrder);

        void DeleteOrder(Order newCustomerOrder);
    }
}