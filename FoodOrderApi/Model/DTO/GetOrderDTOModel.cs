using System.ComponentModel.DataAnnotations;

namespace FoodOrderApi.Model.DTO
{
    public class GetOrderDTO
    {
        [Required]
        public string CustomerName { get; set; }

        [Required]
        public string RestaurantName { get; set; }

        [Required]
        public string ProductName { get; set; }
    }
}