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
    [Route("Restaurant")]
    public class RestaurantController : ControllerBase
    {
        private IDataProvider _dataProvider;
        private readonly IMapper mapper;
        private readonly ILogger logger;

        public RestaurantController(IDataProvider dataProvider, IMapper mapper, ILogger<RestaurantController> logger)
        {
            _dataProvider = dataProvider;
            this.mapper = mapper;
            this.logger = logger;
        }

        //[HttpGet("Restaurants.{format}"), FormatFilter]
        [HttpGet]
        public async Task<ActionResult> GetRestaurant()
        {
            var restaurants = await _dataProvider.GetRestaurant();
            logger.LogInformation("success");
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
            //await Response.Body.WriteAsync("ArunAar");

            var OrderDetails = await _dataProvider.PlaceOrder(newCustomerOrder);
            Console.WriteLine("Order Placed");
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

        [HttpDelete("OrderDelivered/{orderId}")]
        public async Task<ActionResult> OrderDelivered(Guid orderId)
        {
            var IsDelivered = _dataProvider.OrderDelivered(orderId);
            if (IsDelivered.Result)
            {
                return Ok("Success");
            }
            else
            {
                return NotFound("Can't able to found the Order.");
            }
        }
    }
}