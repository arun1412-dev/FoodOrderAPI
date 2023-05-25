using FoodOrderApi.Model.Domain;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace FoodOrderApi.Model.DTO
{
    public class MenuDTO
    {
        public MenuDTO()
        {
        }

        [JsonConstructor]
        public MenuDTO(int productID, int restaurantID, string productName, string productPrice)
        {
            ProductID = productID;
            RestaurantID = restaurantID;
            ProductName = productName;
            ProductPrice = productPrice;
        }

        public int id { get; set; }

        [Key]
        public int ProductID { get; set; }

        public int RestaurantID { get; set; }
        public string ProductName { get; set; }
        public string ProductPrice { get; set; }

        //Navigation
        public Restaurant Restaurant { get; set; }
    }
}