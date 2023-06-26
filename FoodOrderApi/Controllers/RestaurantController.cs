using AutoMapper;
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
                return NotFound("Restaurants not found!");
            }
        }

        [HttpGet("RestaurantByName")]
        public async Task<ActionResult> GetRestaurantByName([FromQuery] string? filterString)
        {
            var restaurants = (await _dataProvider.FilterRestaurant(filterString));
            var restaurantMapper = mapper.Map<List<DisplayRestaurantDTO>>(restaurants.ToList());
            return Ok(restaurantMapper);
        }

        [HttpGet("Search")]
        public async Task<ActionResult> SearchRestaurantandMenu([FromQuery][Required] string searchString)
        {
            if (searchString.Trim().Length != 0)
            {
                var searched = (await _dataProvider.SearchMenuAndRestaurant(searchString.Trim()));
                var restaurantDTOMapper = mapper.Map<List<RestaurantDTO>>(searched.restaurant);
                var menuDTOMapper = mapper.Map<List<DisplayMenuDTO>>(searched.menu);
                RestaurantsandMenusDTO restaurantsandMenusDTOs = new RestaurantsandMenusDTO();
                restaurantsandMenusDTOs.menu = menuDTOMapper;
                restaurantsandMenusDTOs.restaurant = restaurantDTOMapper;
                if (restaurantsandMenusDTOs.menu.Count() > 0 && restaurantsandMenusDTOs.restaurant.Count() > 0)
                {
                    return Ok(restaurantsandMenusDTOs);
                }
                else if (searched.menu.Count() > 0)
                {
                    return Ok(restaurantsandMenusDTOs.menu);
                }
                else if (restaurantsandMenusDTOs.restaurant.Count() > 0)
                {
                    return Ok(restaurantsandMenusDTOs.restaurant);
                }
            }
            return NotFound();
        }

        [HttpGet("Menu")]
        public async Task<ActionResult> GetMenu()
        {
            var menus = await (_dataProvider.GetMenus());
            var menusMapper = mapper.Map<List<DisplayMenuDTO>>(menus.ToList());
            logger.LogInformation("Data fetched from the Menu table.");
            return Ok(menus);
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
    }
}