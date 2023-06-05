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
            var restaurantMapper = mapper.Map<List<DisplayRestaurantDTO>>(restaurants.ToList());
            logger.LogInformation("Data fetched from the restaurant table.");
            return Ok(restaurantMapper);
        }

        [HttpGet("RestaurantwithMenus")]
        public async Task<ActionResult> GetRestaurantWithMenu([Required] string RestrauntName)
        {
            var restaurantsWithMenu = await _dataProvider.GetRestaurantWithMenu(RestrauntName);
            if (restaurantsWithMenu == null)
            {
                logger.LogError("Restaurant not found in the database.");
                return NotFound("Restaurant not found.");
            }
            var restaurantMapper = mapper.Map<List<string>>(restaurantsWithMenu.ToList());
            logger.LogInformation("Data fetched from the Restaurant with Menu table.");
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
            logger.LogInformation("Data fetched from the Menu table.");
            return Ok(menusMapper);
        }

        [HttpPost("PlaceOrder")]
        public async Task<ActionResult> PlaceOrder([FromBody] List<GetOrderDTO> newCustomerOrder)
        {
            var newor = newCustomerOrder;
            var OrderDetails = await _dataProvider.PlaceOrder(newCustomerOrder);
            if (OrderDetails == null)
            {
                logger.LogError("Can't able to add data as they are invalid.");
                return BadRequest();
            }
            logger.LogInformation("Data added to the Orders Table.");
            return Ok(mapper.Map<List<OrderDTO>>(OrderDetails));
        }

        [HttpGet("Orders/{customerName}")]
        public async Task<ActionResult> GetOrders([Required] string customerName)
        {
            var getOrders = await _dataProvider.GetOrderByName(customerName);
            if (getOrders == null)
            {
                logger.LogInformation("Orders not found for the particular person.");
                return NotFound("Orders not found.");
            }
            logger.LogInformation("Data fetched from the Orders Table.");
            return Ok(mapper.Map<IEnumerable<OrderDTO>>(getOrders));
        }

        [HttpDelete("OrderDelivered/{orderId}")]
        public async Task<ActionResult> OrderDelivered(Guid orderId)
        {
            var IsDelivered = _dataProvider.OrderDelivered(orderId);
            if (IsDelivered.Result)
            {
                logger.LogInformation("Order delivered and changed in the database.");
                return Ok("Success");
            }
            else
            {
                logger.LogInformation("Order not found.");
                return NotFound("Can't able to found the Order.");
            }
        }
        [HttpGet("Discount/{restaturant}/{discount}")]
        public async Task<ActionResult> Discount( [FromRoute] string restaturant, [FromRoute] double discount){
            if (_dataProvider.Discount(restaturant, discount).Result)
            {
                return Ok("success");
            }
            else
            {
                return NotFound("Restaturant Not Found");
            }
        }
    }
}