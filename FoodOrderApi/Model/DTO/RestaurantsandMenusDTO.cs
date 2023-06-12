namespace FoodOrderApi.Model.DTO
{
    public class RestaurantsandMenusDTO
    {
        public List<DisplayMenuDTO> menu { get; set; }
        public List<DisplayRestaurantDTO> restaurant { get; set; }
    }
}