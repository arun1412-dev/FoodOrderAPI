using System.ComponentModel.DataAnnotations;

namespace FoodOrderApi.Model
{
    public class Menu
    {
        [Key] public int Id { get; set; }
        public int ProductID { get; set; }

        public int RestaurantID { get; set; }
        public string ProductName { get; set; }
        public string ProductPrice { get; set; }
    }
}