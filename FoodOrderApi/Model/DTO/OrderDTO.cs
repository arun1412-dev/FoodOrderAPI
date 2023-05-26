using FoodOrderApi.Model.Domain;

namespace FoodOrderApi.Model.DTO
{
    public class OrderDTO
    {
        public int Id { get; set; }
        public Guid RestaurantID { get; set; }
        public Guid ProductID { get; set; }
        public string CustomerName { get; set; }

        //Navigation
        public Restaurant Restaurant { get; set; }

        public Menu Menu { get; set; }
    }
}