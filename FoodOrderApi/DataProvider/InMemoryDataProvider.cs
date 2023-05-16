//using FoodOrderApi.Model;

//namespace FoodOrderApi.DataProvider
//{
//    public class InMemoryDataProvider : IDataProvider
//    {
//        //List<Customer> _customerList;
//        private List<Restaurant> _restaurantList;

//        private List<OrderDisplay> _orderDisplayList;
//        public static List<Customer> _CustomerOrder = new List<Customer>();
//        //List<Customer> _CustomerOrder = new List<Customer>();

//        private Dictionary<int, List<Menu>> _menuList;
//        //Dictionary<string, List<Menu>> _CustomerOrder;
//        //List<Menu> _menuList;

//        public InMemoryDataProvider()
//        {
//            RestaurantList();
//            MenuList();
//        }

//        public void RestaurantList()
//        {
//            _restaurantList = new List<Restaurant>();
//            _restaurantList.Add(new Restaurant() { RestaurantID = 1, RestaurantName = "Kove", RestaurantType = "Fine Dining", RestaurantPhoneNumber = 1234567890, RestaurantLocation = "RS Puram" });
//            _restaurantList.Add(new Restaurant() { RestaurantID = 2, RestaurantName = "Annapoorna", RestaurantType = "Casual Dining", RestaurantPhoneNumber = 1234567890, RestaurantLocation = "RS Puram" });
//            _restaurantList.Add(new Restaurant() { RestaurantID = 3, RestaurantName = "Chocolate Room", RestaurantType = "Desserts and Beerages", RestaurantPhoneNumber = 1234567890, RestaurantLocation = "Race Course Road" });
//            _restaurantList.Add(new Restaurant() { RestaurantID = 4, RestaurantName = "Orbis", RestaurantType = "Multi Cuisine", RestaurantPhoneNumber = 1234567890, RestaurantLocation = "Avinashi Road" });
//        }

//        public void MenuList()
//        {
//            _menuList = new Dictionary<int, List<Menu>>();
//            _menuList.Add(1, new List<Menu>() { new Menu() { ProductID = 1, ProductName = "Thai Broccoli Salad", ProductPrice = "255" }, new Menu() { ProductID = 2, ProductName = "Chettinad Cutlet", ProductPrice = "275" }, new Menu() { ProductID = 3, ProductName = "Curd Rice Aracini", ProductPrice = "265" } });
//            _menuList.Add(2, new List<Menu>() { new Menu() { ProductID = 1, ProductName = "Idly(2)", ProductPrice = "33" }, new Menu() { ProductID = 2, ProductName = "Pongal", ProductPrice = "48" }, new Menu() { ProductID = 3, ProductName = "Roast", ProductPrice = "75" } });
//            _menuList.Add(3, new List<Menu>() { new Menu() { ProductID = 1, ProductName = "Chocolate Sizzler", ProductPrice = "299" }, new Menu() { ProductID = 2, ProductName = "Choco Brownie Bomb", ProductPrice = "249" }, new Menu() { ProductID = 3, ProductName = "Chocolate Cookies", ProductPrice = "199" } });
//            _menuList.Add(4, new List<Menu>() { new Menu() { ProductID = 1, ProductName = "Vegetable Club Sandwich", ProductPrice = "150" }, new Menu() { ProductID = 2, ProductName = "Veg Combo Meal", ProductPrice = "439" }, new Menu() { ProductID = 3, ProductName = "Orbis Signature Rice", ProductPrice = "250" } });
//        }

//        public IEnumerable<Customer> GetOrder(string customerName)
//        {
//            return _CustomerOrder;
//        }

//        //public IEnumerable<Customer> GetCustomer()
//        //{
//        //    return Enumerable.Empty<Customer>();
//        //}

//        public IEnumerable<Restaurant> GetRestaurant()
//        {
//            return _restaurantList;
//        }

//        public Dictionary<int, List<Menu>> GetMenu(int id)
//        {
//            return _menuList;
//        }

//        public Dictionary<int, List<Menu>> GetMenuDisplay()
//        {
//            return _menuList;
//        }

//        public IEnumerable<Customer> GetCustomerOrders()
//        {
//            if (_CustomerOrder.Count > 0)
//            {
//                return _CustomerOrder;
//            }
//            else
//            {
//                return Enumerable.Empty<Customer>();
//            }
//        }

//        //public IEnumerable<OrderDisplay> GetOrderDisplays(Customer order)
//        //{
//        //    return _orderDisplayList;
//        //}

//        public void PlaceOrder(Customer newCustomerOrder)
//        {
//            //_CustomerOrder = new Dictionary<string, List<Customer>>();
//            //_CustomerOrder.Add(newCustomerOrder.CustomerName, );
//            //_CustomerOrder.Add(newCustomerOrder);
//            //_CustomerOrder = new List<Customer>();

//            _CustomerOrder.Add(newCustomerOrder);
//        }

//        //public IEnumerable<Menu> GetOrder()
//        //{
//        //    return _CustomerOrder;
//        //}
//    }
//}