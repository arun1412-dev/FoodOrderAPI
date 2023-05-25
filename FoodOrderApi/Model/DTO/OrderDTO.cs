using FoodOrderApi.Model.Domain;

namespace FoodOrderApi.Model.DTO
{
    public class OrderDTO
    {
        public int Id { get; set; }
        public int RestaurantID { get; set; }
        public int ProductID { get; set; }
        public string CustomerName { get; set; }

        //Navigation
        public Restaurant Restaurant { get; set; }

        public Menu Menu { get; set; }
    }
}