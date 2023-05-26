using System.ComponentModel.DataAnnotations;

namespace FoodOrderApi.Model.DTO
{
    public class RestaurantWithMenuDTO
    {
        [Key]
        public int RestaurantID { get; set; }

        public string RestaurantName { get; set; }
        public string RestaurantType { get; set; }
        public int RestaurantPhoneNumber { get; set; }
        public string RestaurantLocation { get; set; }
        public IList<string> Menus { get; set; }
    }
}