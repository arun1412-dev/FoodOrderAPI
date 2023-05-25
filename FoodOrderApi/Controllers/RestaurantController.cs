using FoodOrderApi.DataProvider;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace FoodOrderApi.Controllers
{
    [Route("Restaurants")]
    public class RestaurantController : ControllerBase
    {
        private IDataProvider _dataProvider;

        public RestaurantController(IDataProvider dataProvider)
        {
            _dataProvider = dataProvider;
        }

        [HttpGet]
        public async Task<ActionResult> GetRestaurant()
        {
            var restaurants = await _dataProvider.GetRestaurant();
            return Ok(restaurants.ToList());
        }

        [HttpGet("RestaurantwithMenus")]
        public async Task<ActionResult> GetRestaurantWithMenu([Required] string RestrauntName)
        {
            var restaurantsWithMenu = await _dataProvider.GetRestaurantWithMenu(RestrauntName);
            if (restaurantsWithMenu == null)
            {
                return NotFound("Restaurant not found.");
            }
            return Ok(restaurantsWithMenu);
        }

        [HttpGet("Menu")]
        public async Task<ActionResult> GetMenu()
        {
            var menus = await _dataProvider.GetMenus();
            return Ok(menus.ToList());
        }

        //[HttpPost("PlaceOrder")]
        //public ActionResult PlaceOrder(GetOrderDTOModel newCustomerOrder)
        //{
        //    List<Restaurant> restaurants = null; //_dataProvider.GetRestaurant().Where(item => item.RestaurantName.ToLower() == newCustomerOrder.RestaurantName.ToLower()).ToList();
        //    if (restaurants.Count() > 0)
        //    {
        //        List<Menu> products = _dataProvider.GetMenus().Where(item => item.ProductName.ToLower() == newCustomerOrder.ProductName.ToLower()).ToList();
        //        if (products.Count() > 0 && products[0].RestaurantID == restaurants[0].RestaurantID)
        //        {
        //            var newOrder = new Order();
        //            newOrder.CustomerName = newCustomerOrder.CustomerName;
        //            newOrder.RestaurantID = products[0].RestaurantID;
        //            newOrder.ProductID = products[0].ProductID;
        //            _dataProvider.PlaceOrder(newOrder);
        //            return Ok("Order Placed.");
        //        }
        //        else
        //        {
        //            return NotFound("Product Not found.");
        //        }
        //    }
        //    else
        //    {
        //        return NotFound("Restaurnt Not found.");
        //    }
        //}

        //[HttpGet("Orders/{customerName}")]
        //public ActionResult Order(string customerName)
        //{
        //    var customerOrder = _dataProvider.GetOrderByName(customerName).ToList();
        //    var orderList = new List<DisplayOrderDTOModel>();
        //    if (customerOrder.Count > 0)
        //    {
        //        foreach (Order order in customerOrder)
        //        {
        //            foreach (Menu menu in _dataProvider.GetMenus())
        //            {
        //                if (menu.RestaurantID == order.RestaurantID && menu.ProductID == order.ProductID)
        //                {
        //                    var restaurant = _dataProvider.GetRestaurant().Where(item => item.RestaurantID == menu.RestaurantID).ToList();
        //                    var newOrder = new DisplayOrderDTOModel();
        //                    newOrder.RestaurantName = restaurant[0].RestaurantName;
        //                    newOrder.ProductName = menu.ProductName;
        //                    newOrder.ProductPrice = menu.ProductPrice;
        //                    newOrder.RestaurantPhoneNumber = restaurant[0].RestaurantPhoneNumber;
        //                    newOrder.RestaurantLocation = restaurant[0].RestaurantLocation;
        //                    newOrder.RestaurantType = restaurant[0].RestaurantType;
        //                    orderList.Add(newOrder);
        //                }
        //            }
        //        }
        //        return Ok(orderList);
        //    }
        //    else
        //    {
        //        return NotFound("Order Not found.");
        //    }
        //}

        //[HttpDelete("DeleteOrder/{customerName}")]
        //public ActionResult DeleteOrder(string customerName, string restrauntID, string productID)
        //{
        //    bool flag = true;
        //    var customerOrder = _dataProvider.GetOrderByName(customerName).ToList();
        //    if (customerOrder.Count > 0)
        //    {
        //        foreach (Order order in customerOrder)
        //        {
        //            if (order.RestaurantID.ToString() == restrauntID && order.ProductID.ToString() == productID)
        //            {
        //                _dataProvider.DeleteOrder(order);
        //                flag = false;
        //            }
        //        }
        //        if (flag)
        //        {
        //            return NotFound("Can't able to found the Order.");
        //        }
        //        return Ok("Success");
        //    }
        //    else
        //    {
        //        return NotFound("Can't able to found the Order.");
        //    }
        //}
    }
}