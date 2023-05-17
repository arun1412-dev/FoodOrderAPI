using System.ComponentModel.DataAnnotations;

namespace FoodOrderApi.Model
{
    public class CustomerDTOModel
    {
        [Required]
        public string CustomerName { get; set; }

        [Required]
        public string RestaurantName { get; set; }

        [Required]
        public string ProductName { get; set; }
    }
}