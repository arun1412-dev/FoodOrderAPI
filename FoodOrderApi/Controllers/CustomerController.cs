using AutoMapper;
using FoodOrderApi.CustomActionFilter;
using FoodOrderApi.DataProvider;
using FoodOrderApi.Model.Domain;
using FoodOrderApi.Model.DTO;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace FoodOrderApi.Controllers
{
    [Route("Customer")]
    public class CustomerController : ControllerBase
    {
        private IDataProvider _dataProvider;
        private readonly IMapper mapper;
        private readonly ILogger logger;

        public CustomerController(IDataProvider dataProvider, IMapper mapper, ILogger<CustomerController> logger)
        {
            _dataProvider = dataProvider;
            this.mapper = mapper;
            this.logger = logger;
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
            if (!getOrders.Any())
            {
                logger.LogInformation("Orders not found for the particular person.");
                return NotFound("Orders not found.");
            }
            logger.LogInformation("Data fetched from the Orders Table.");
            return Ok(mapper.Map<IEnumerable<OrderDTO>>(getOrders));
        }
    }
}