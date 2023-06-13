using AutoMapper;
using FoodOrderApi.CustomActionFilter;
using FoodOrderApi.DataProvider;
using FoodOrderApi.Model.Domain;
using FoodOrderApi.Model.DTO;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

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
        public async Task<ActionResult> GetRestaurant([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 3)
        {
            if (pageSize > maxPageSize)
            {
                pageSize = maxPageSize;
            }
            // Get the restaurants
            var (allRestaurants, metadata) = await _dataProvider.GetRestaurantPaged(pageNumber, pageSize);
            var restaurantMapper = mapper.Map<List<DisplayRestaurantDTO>>(allRestaurants.ToList());
            if (allRestaurants.Count() != 0)
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

        [HttpGet("RestaurantByName")]
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
        [HttpPut("Discount/{restaturant}/{discount}")]
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

        [HttpPatch("PatchMenu/{RestaurantID}")]
        public async Task<ActionResult> PatchMenuItems([Required] Guid RestaurantID, [FromBody] JsonPatchDocument<Menu> jsonPatchDocument)
        {
            var status = await _dataProvider.PatchMenuItems(RestaurantID, jsonPatchDocument);
            if (status != null)
            {
                var statusDTO = mapper.Map<MenuDTO>(status);
                return Ok(statusDTO);
            }
            return BadRequest();
        }
    }
}