using System.ComponentModel.DataAnnotations;

namespace FoodOrderApi.Model
{
    public class Restaurant
    {
        [Key]
        public int RestaurantID { get; set; }
        public string RestaurantName { get; set; }
        public string RestaurantType { get; set; }
        public int RestaurantPhoneNumber { get; set; }
        public string RestaurantLocation { get; set; }
    }
}
