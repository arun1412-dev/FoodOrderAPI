﻿using AutoMapper;
using FoodOrderApi.DataProvider;
using FoodOrderApi.Model.Domain;
using FoodOrderApi.Model.DTO;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace FoodOrderApi.Controllers
{
    [Route("Admin")]
    public class AdminController : ControllerBase
    {
        private IDataProvider _dataProvider;
        private readonly IMapper mapper;
        private readonly ILogger logger;

        public AdminController(IDataProvider dataProvider, IMapper mapper, ILogger<AdminController> logger)
        {
            _dataProvider = dataProvider;
            this.mapper = mapper;
            this.logger = logger;
        }

        [HttpDelete("DeleteMenu/{MenuID:Guid}")]
        public async Task<ActionResult> DeleteMenu([Required] Guid MenuID)
        {
            var IsDeleted = _dataProvider.DeleteMenu(MenuID);
            if (IsDeleted.Result)
            {
                logger.LogInformation("Menu removed from the hotel");
                return Ok("Menu removed from the hote");
            }
            else
            {
                logger.LogInformation("Can't able to found the menu");
                return BadRequest("Can't able to found the menu");
            }
        }

        [HttpDelete("OrderDelivered/{orderId:GUID}")]
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

        [HttpPut("Discount/{restaturant}/{discount}")]
        public async Task<ActionResult> Discount([FromRoute] string restaturant, [FromRoute][Range(0, 100)] double discount)
        {
            if (_dataProvider.Discount(restaturant, discount).Result)
            {
                return Ok("success");
            }
            else
            {
                return NotFound("Restaturant Not Found");
            }
        }

        [HttpPatch("AddMenu/{RestaurantID}")]
        public async Task<ActionResult> AddMenuToTheRestaurant([Required] Guid RestaurantID, [FromBody] JsonPatchDocument<Menu> jsonPatchDocument)
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