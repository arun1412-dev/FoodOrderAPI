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
        public async Task<ActionResult> PlaceOrder(GetOrderDTO newCustomerOrder)
        {
            var OrderDetails = await _dataProvider.PlaceOrder(newCustomerOrder);
            return Ok(mapper.Map<OrderDTO>(OrderDetails));
        }

        [HttpGet("Orders/{customerName}")]
        public async Task<ActionResult> GetOrders([Required] string customerName)
        {
            var getOrders = await _dataProvider.GetOrderByName(customerName);
            if (getOrders == null)
            {
                return NotFound("Orders not found.");
            }
            return Ok(mapper.Map<IEnumerable<OrderDTO>>(getOrders));
        }

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