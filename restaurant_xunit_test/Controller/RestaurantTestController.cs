using AutoMapper;
using FluentAssertions;
using FoodOrderApi.Controllers;
using FoodOrderApi.DataProvider;
using FoodOrderApi.Mappings;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using restaurant_xunit_test.Mockdata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace restaurant_xunit_test.Controller
{
    public class RestaurantTestController
    {
        private Mock<IDataProvider> restaurantService;
        private IMapper mapperMock;
        private Mock<ILogger<RestaurantController>> loggerMock;

        public RestaurantTestController()
        {
            restaurantService = new Mock<IDataProvider>();
            loggerMock = new Mock<ILogger<RestaurantController>>();
            if (mapperMock == null)
            {
                var mappingConfig = new MapperConfiguration(mc =>
                {
                    mc.AddProfile(new AutoMapperProfiles());
                });
                IMapper mapper = mappingConfig.CreateMapper();
                mapperMock = mapper;
            }
        }

        [Theory]
        [InlineData(1, 10)]
        [InlineData(19, 2)]
        public async Task GetAllRestaurantPagination_ShouldReturn200Status(int pageNumber, int pageSize)
        {
            /// Arrange
            restaurantService.Setup(_ => _.GetRestaurantPaged(pageNumber, pageSize)).ReturnsAsync(RestaurantMockData.GetRestaurantPaged(pageNumber, pageSize));
            var sut = new RestaurantController(restaurantService.Object, mapperMock, loggerMock.Object);
            //sut.Response.Headers = new Dictionary<string, string>();
            sut.ControllerContext = new ControllerContext();
            sut.ControllerContext.HttpContext = new DefaultHttpContext();

            /// Act
            var result = await sut.GetRestaurant(pageNumber, pageSize);

            // /// Assert
            result.Should().BeOfType<OkObjectResult>().Which.StatusCode.Should().Be((int)HttpStatusCode.OK);
        }

        [Theory]
        [InlineData("Annapoorna")]
        [InlineData("Bhavan")]
        [InlineData("")]
        public async Task GetFilteredRestaurant_ShouldReturn200Status(string filterString)
        {
            /// Arrange
            restaurantService.Setup(_ => _.FilterRestaurant(filterString)).ReturnsAsync(RestaurantMockData.GetFilteredRestaurants(filterString));
            var sut = new RestaurantController(restaurantService.Object, mapperMock, loggerMock.Object);

            /// Act
            var result = await sut.GetRestaurantByName(filterString);

            // /// Assert
            result.Should().BeOfType<OkObjectResult>().Which.StatusCode.Should().Be((int)HttpStatusCode.OK);
        }

        [Theory]
        [InlineData("Annapoorna")]
        [InlineData("Bhavan")]
        [InlineData("")]
        public async Task SearchRestaurantandMenu_ShouldReturn200Status(string searchString)
        {
            /// Arrange
            restaurantService.Setup(_ => _.SearchMenuAndRestaurant(searchString)).ReturnsAsync(RestaurantMockData.GetSearchRestaurantAndMenu(searchString));
            var sut = new RestaurantController(restaurantService.Object, mapperMock, loggerMock.Object);

            /// Act
            var result = await sut.SearchRestaurantandMenu(searchString);

            // /// Assert
            result.Should().BeOfType<OkObjectResult>().Which.StatusCode.Should().Be((int)HttpStatusCode.OK);
        }

        [Fact]
        public async Task GetAllMenu_ShouldReturn200Status()
        {
            /// Arrange
            restaurantService.Setup(_ => _.GetMenus()).ReturnsAsync(RestaurantMockData.GetAllMenus());
            var sut = new RestaurantController(restaurantService.Object, mapperMock, loggerMock.Object);

            /// Act
            var result = await sut.GetMenu();

            // /// Assert
            result.Should().BeOfType<OkObjectResult>().Which.StatusCode.Should().Be((int)HttpStatusCode.OK);
        }
    }
}