using AutoMapper;
using FoodOrderApi.CustomActionFilter;
using FoodOrderApi.DataProvider;
using FoodOrderApi.Model.DTO;
using Microsoft.AspNetCore.Mvc;
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
        public async Task<ActionResult> GetRestaurantByName([FromQuery] string? filterString)
        {
            var restaurants = (await _dataProvider.FilterRestaurant(filterString));
            var restaurantMapper = mapper.Map<List<DisplayRestaurantDTO>>(restaurants.ToList());
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

        [HttpGet("Search")]
        public async Task<ActionResult> SearchRestaurantandMenu([FromQuery][Required] string searchString)
        {
            if (searchString.Trim().Length != 0)
            {
                var searched = (await _dataProvider.SearchMenuAndRestaurant(searchString.Trim()));
                if (searched.menu.Count() > 0 && searched.restaurant.Count() > 0)
                {
                    // var menumapper = mapper.Map<List<DisplayMenuDTO>>(searched);
                    return Ok(searched);
                }
                else if (searched.menu.Count() > 0)
                {
                    return Ok(searched.menu);
                }
                else if (searched.restaurant.Count() > 0)
                {
                    return Ok(searched.restaurant);
                }
            }
            return BadRequest();
        }

        [HttpGet("Menu")]
        public async Task<ActionResult> GetMenu()
        {
            var menus = await (_dataProvider.GetMenus());
            var menusMapper = mapper.Map<List<DisplayMenuDTO>>(menus.ToList());
            logger.LogInformation("Data fetched from the Menu table.");
            return Ok(menus);
        }

        [HttpPost("PlaceOrder")]
        [ValidateModel]
        public async Task<ActionResult> PlaceOrder([FromBody] List<GetOrderDTO> newCustomerOrder)
        {
            var newor = newCustomerOrder;
            var OrderDetails = await _dataProvider.PlaceOrder(newCustomerOrder);
            if (OrderDetails == null)
            {
                logger.LogError("Can't able to add data as they are invalid.");
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
        public async Task<ActionResult> OrderDelivered([Required] Guid orderId)
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

        [HttpDelete("DeleteMenu/{MenuID:Guid}")]
        public async Task<ActionResult> DeleteMenu([Required] Guid MenuID)
        {
            var IsDeleted = _dataProvider.DeleteMenu(MenuID);
            if (IsDeleted.Result)
            {
                logger.LogInformation("Menu removed from the hotel");
                return Ok("Success");
            }
            else
            {
                logger.LogInformation("Order not found.");
                return BadRequest("Can't able to found the Order.");
            }
        }
    }
}