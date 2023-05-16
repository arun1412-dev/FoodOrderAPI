using System.ComponentModel.DataAnnotations;

namespace FoodOrderApi.Model
{
    public class CustomerDTOModel
    {
        [Required]
        public string CustomerName { get; set; }

        [Required]
        public int RestaurantID { get; set; }

        [Required]
        public int ProductID { get; set; }
    }
}