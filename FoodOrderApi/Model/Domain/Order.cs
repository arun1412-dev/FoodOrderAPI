namespace FoodOrderApi.Model.Domain
{
    public class Order
    {
        public Guid Id { get; set; }
        public Guid RestaurantID { get; set; }
        public Guid ProductID { get; set; }
        public string CustomerName { get; set; }

        //Navigation
        public Restaurant Restaurant { get; set; }

        public Menu Menu { get; set; }
    }
}