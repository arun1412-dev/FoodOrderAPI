using Microsoft.AspNetCore.Mvc;
using FoodOrderApi.Model;
using FoodOrderApi.DataProvider;
using System.ComponentModel.Design;
using System.Linq;

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
        public ActionResult GetRestaurant()
        {
            return Ok(_dataProvider.GetRestaurant());
        }

        [HttpGet("Menu")]
        public ActionResult GetMenu()
        {
            return Ok(_dataProvider.GetMenuDisplay());
        }

        [HttpPatch("PlaceOrder")]
        public void PlaceOrder(CustomerDTOModel newCustomerOrder)
        {
            var newOrder = new Order();
            newOrder.CustomerName = newCustomerOrder.CustomerName;
            newOrder.RestaurantID = newCustomerOrder.RestaurantID;
            newOrder.ProductID = newCustomerOrder.ProductID;
            _dataProvider.PlaceOrder(newOrder);
        }

        [HttpGet("Orders/{customerName}")]
        public ActionResult GetOrder(string customerName)
        {
            var customerOrder = _dataProvider.GetOrderByName(customerName).ToList();
            var orderList = new List<OrderDisplay>();
            if (customerOrder.Count > 0)
            {
                foreach (Order order in customerOrder)
                {
                    foreach (Restaurant restaurant in _dataProvider.GetRestaurant())
                    {
                        if (restaurant.RestaurantID == order.RestaurantID)
                        {
                            foreach (Menu menu in _dataProvider.GetMenuDisplay())
                            {
                                if (menu.RestaurantID == order.RestaurantID && menu.ProductID == order.RestaurantID)
                                {
                                    var newOrder = new OrderDisplay();
                                    newOrder.RestaurantName = restaurant.RestaurantName;
                                    newOrder.ProductName = menu.ProductName;
                                    newOrder.ProductPrice = menu.ProductPrice;
                                    newOrder.RestaurantPhoneNumber = restaurant.RestaurantPhoneNumber;
                                    newOrder.RestaurantLocation = restaurant.RestaurantLocation;
                                    newOrder.RestaurantType = restaurant.RestaurantType;
                                    orderList.Add(newOrder);
                                }
                            }
                        }
                    }
                }
                return Ok(orderList);
            }
            else
            {
                return BadRequest("Not found");
            }
        }

        [HttpDelete("DeleteOrder/{customerName}")]
        public ActionResult DeleteOrder(string customerName, string restrauntID, string productID)
        {
            bool flag = true;
            var customerOrder = _dataProvider.GetOrderByName(customerName).ToList();
            if (customerOrder.Count > 0)
            {
                foreach (Order order in customerOrder)
                {
                    if (order.RestaurantID.ToString() == restrauntID && order.ProductID.ToString() == productID)
                    {
                        _dataProvider.DeleteOrder(order);
                        flag = false;
                    }
                }
                if (flag)
                {
                    return NotFound("Can't able to found the Order.");
                }
                return Ok("Success");
            }
            else
            {
                return NotFound("Can't able to found the Order.");
            }
        }
    }
}