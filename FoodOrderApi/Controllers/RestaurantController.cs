using AutoMapper;
using FoodOrderApi.DataProvider;
using FoodOrderApi.Model.Domain;
using FoodOrderApi.Model.DTO;
using FoodOrderApi.Repository;
using Microsoft.AspNetCore.Mvc;
using ServiceStack;
using System.ComponentModel.DataAnnotations;

namespace FoodOrderApi.Controllers
{
    [Route("Restaurants")]
    public class RestaurantController : ControllerBase
    {
        private IDataProvider _dataProvider;
        private readonly IMapper mapper;

        public RestaurantController(IDataProvider dataProvider, IMapper mapper)
        {
            _dataProvider = dataProvider;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult> GetRestaurant()
        {
            var restaurants = await _dataProvider.GetRestaurant();

            var restaurantMapper = mapper.Map<List<DisplayRestaurantDTO>>(restaurants.ToList());
            return Ok(restaurantMapper);
        }

        [HttpGet("RestaurantwithMenus")]
        public async Task<ActionResult> GetRestaurantWithMenu([Required] string RestrauntName)
        {
            var restaurantsWithMenu = await _dataProvider.GetRestaurantWithMenu(RestrauntName);
            if (restaurantsWithMenu == null)
            {
                return NotFound("Restaurant not found.");
            }
            var restaurantMapper = mapper.Map<List<string>>(restaurantsWithMenu.ToList());
            return Ok(restaurantMapper);
        }

        [HttpGet("Menu")]
        public async Task<ActionResult> GetMenu()
        {
            var menus = await (_dataProvider.GetMenus());
            var menusMapper = mapper.Map<List<DisplayMenuDTO>>(menus.ToList());
            var listOfRestaurants = (await _dataProvider.GetRestaurant()).ToList();
            for (int menuindex = 0; menuindex < menus.Count(); menuindex++)
            {
                menusMapper[menuindex].RestaurantName = listOfRestaurants
                    .FirstOrDefault(x => x.RestaurantID == menus.ToList()[menuindex].RestaurantID).RestaurantName;
            }
            return Ok(menusMapper);
        }

        [HttpPost("PlaceOrder")]
        public ActionResult PlaceOrder(GetOrderDTO newCustomerOrder)
        {
            var OrderDetails = _dataProvider.PlaceOrder(newCustomerOrder);
            return Ok(OrderDetails);
        }

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