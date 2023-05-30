using FoodOrderApi.Model.Domain;

namespace FoodOrderApi.Model.DTO
{
    public class OrderDTO
    {
        public Guid OrderID { get; set; }

        //Navigation
        public RestaurantDTO Restaurant { get; set; }

        public MenuDTO Menu { get; set; }
    }
}