using System.Text.Json.Serialization;

namespace FoodOrderApi.Model.Domain
{
    public class Menu
    {
        public Menu()
        {
        }

        [JsonConstructor]
        public Menu(int productID, int restaurantID, string productName, string productPrice)
        {
            ProductID = productID;
            RestaurantID = restaurantID;
            ProductName = productName;
            ProductPrice = productPrice;
        }

        public int Id { get; set; }
        public int ProductID { get; set; }
        public int RestaurantID { get; set; }
        public string ProductName { get; set; }
        public string ProductPrice { get; set; }
    }
}