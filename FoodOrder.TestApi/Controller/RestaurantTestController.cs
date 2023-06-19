using AutoMapper;
using FluentAssertions;
using FoodOrderApi.Controllers;
using FoodOrderApi.DataProvider;
using FoodOrderApi.Mappings;
using FoodOrderApi.Model.DTO;
using FoodOrderApi.TestApi.Mockdata;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System.Net;

namespace FoodOrderApi.TestApi.Controller
{
    public class TestRestaurantController
    {
        private Mock<IDataProvider> restaurantService;
        private IMapper mapperMock;
        private Mock<ILogger<RestaurantController>> loggerMock;

        public TestRestaurantController()
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
        [InlineData(3, 2)]
        public async Task Get_AllRestaurantPaginationWithValidIndex__ShouldReturn200Status(int pageNumber, int pageSize)
        {
            /// Arrange
            restaurantService.Setup(_ => _.GetRestaurantPaged(pageNumber, pageSize)).ReturnsAsync(RestaurantMockData.GetRestaurantPaged(pageNumber, pageSize));
            var sut = new RestaurantController(restaurantService.Object, mapperMock, loggerMock.Object);
            //sut.Response.Headers = new Dictionary<string, string>();
            sut.ControllerContext = new ControllerContext();
            sut.ControllerContext.HttpContext = new DefaultHttpContext();

            /// Act
            var result = await sut.GetRestaurant(pageNumber, pageSize);
            var model = (result as OkObjectResult).Value as List<DisplayRestaurantDTO>;
            // /// Assert
            model.Should().HaveCount(RestaurantMockData.GetRestaurantPaged(pageNumber, pageSize).Item1.Count());
            result.Should().BeOfType<OkObjectResult>().Which.StatusCode.Should().Be((int)HttpStatusCode.OK);
        }

        [Theory]
        [InlineData(11, 10)]
        [InlineData(10, 7)]
        public async Task Get_AllRestaurantPaginationWithInvalidIndex__ShouldReturn200Status(int pageNumber, int pageSize)
        {
            /// Arrange
            restaurantService.Setup(_ => _.GetRestaurantPaged(pageNumber, pageSize)).ReturnsAsync(RestaurantMockData.GetRestaurantPaged(pageNumber, pageSize));
            var sut = new RestaurantController(restaurantService.Object, mapperMock, loggerMock.Object);
            sut.ControllerContext = new ControllerContext();
            sut.ControllerContext.HttpContext = new DefaultHttpContext();

            /// Act
            var result = await sut.GetRestaurant(pageNumber, pageSize);

            // /// Assert
            result.Should().BeOfType<NotFoundObjectResult>().Which.StatusCode.Should().Be((int)HttpStatusCode.NotFound);
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
        public async Task SearchRestaurantandMenu_ValidSearchString_ShouldReturn200Status(string searchString)
        {
            /// Arrange
            restaurantService.Setup(_ => _.SearchMenuAndRestaurant(searchString)).ReturnsAsync(RestaurantMockData.GetSearchRestaurantAndMenu(searchString));
            var sut = new RestaurantController(restaurantService.Object, mapperMock, loggerMock.Object);

            /// Act
            var result = await sut.SearchRestaurantandMenu(searchString);

            // /// Assert
            result.Should().BeOfType<OkObjectResult>().Which.StatusCode.Should().Be((int)HttpStatusCode.OK);
        }

        [Theory]
        [InlineData("")]
        public async Task SearchRestaurantandMenu_InvalidSearchString_ShouldReturn200Status(string searchString)
        {
            /// Arrange
            restaurantService.Setup(_ => _.SearchMenuAndRestaurant(searchString)).ReturnsAsync(RestaurantMockData.GetSearchRestaurantAndMenu(searchString));
            var sut = new RestaurantController(restaurantService.Object, mapperMock, loggerMock.Object);

            /// Act
            var result = await sut.SearchRestaurantandMenu(searchString);

            // /// Assert
            result.Should().BeOfType<NotFoundResult>().Which.StatusCode.Should().Be((int)HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task Get_AllMenu_ShouldReturn200Status()
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