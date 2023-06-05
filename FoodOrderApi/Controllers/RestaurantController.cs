using AutoMapper;
using FoodOrderApi.DataProvider;
using FoodOrderApi.Model.Domain;
using FoodOrderApi.Model.DTO;
using FoodOrderApi.Repository;
using Microsoft.AspNetCore.Mvc;
using ServiceStack;
using ServiceStack.Text;
using System.ComponentModel.DataAnnotations;
using System.Text.Json;

namespace FoodOrderApi.Controllers
{
    [Route("Restaurant")]
    public class RestaurantController : ControllerBase
    {
        private IDataProvider _dataProvider;
        private int maxPageSize = 10;
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
        public async Task<ActionResult> GetRestaurant([FromQuery]int pageNumber = 1, [FromQuery] int pageSize = 3)
        {
            if(pageSize > maxPageSize) 
            {
                pageSize = maxPageSize;
            }
            // Get the restaurants
            var (allRestaurants, metadata) = await _dataProvider.GetRestaurantPaged(pageNumber, pageSize);
            var restaurantMapper = mapper.Map<List<DisplayRestaurantDTO>>(allRestaurants.ToList());
            if(allRestaurants.Count()!=0)
            {
                logger.LogInformation("Data fetched from the restaurant table.");
                var serializerOutput = System.Text.Json.JsonSerializer.Serialize(metadata);
                Response.Headers.Add("X-Pagination", serializerOutput);
                return Ok(restaurantMapper);
            }
            else
            {
                Response.Headers.Add("X-Pagination", System.Text.Json.JsonSerializer.Serialize(metadata));
                return BadRequest("Restaurants not found!");
            }
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
    }
}