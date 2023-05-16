namespace FoodOrderApi.Model
{
    public class Order
    {
        public int Id { get; set; }
        public int RestaurantID { get; set; }
        public int ProductID { get; set; }
        public string CustomerName { get; set; }
    }
}