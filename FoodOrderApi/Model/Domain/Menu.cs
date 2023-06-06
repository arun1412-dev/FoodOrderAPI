using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace FoodOrderApi.Model.Domain
{
    public class Menu
    {
        public Menu()
        {
        }

        [JsonConstructor]
        public Menu(Guid productID, Guid restaurantID, string productName, double productPrice)
        {
            ProductID = productID;
            RestaurantID = restaurantID;
            ProductName = productName;
            ProductPrice = productPrice;
        }

        [Key]
        public Guid ProductID { get; set; }

        public Guid RestaurantID { get; set; }
        public string ProductName { get; set; }
        public double ProductPrice { get; set; }
        public Restaurant Restaurant { get; set; }
    }
}