using AutoMapper;
using FluentAssertions;
using FoodOrderApi.Controllers;
using FoodOrderApi.DataProvider;
using FoodOrderApi.Mappings;
using FoodOrderApi.Repository;
using FoodOrderApi.TestApi.Mockdata;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;

namespace FoodOrderApi.TestApi.Services
{
    public class RestaurantServiceTests : IDisposable
    {
        protected readonly FakeFoodApiDbContext _context;
        private IMapper _mapperMock;
        private Mock<ILogger<RestaurantController>> _loggerMock;

        public RestaurantServiceTests()
        {
            var options = new DbContextOptionsBuilder<FakeFoodApiDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            _context = new FakeFoodApiDbContext(options);
            if (_mapperMock == null)
            {
                var mappingConfig = new MapperConfiguration(mc =>
                {
                    mc.AddProfile(new AutoMapperProfiles());
                });
                IMapper mapper = mappingConfig.CreateMapper();
                _mapperMock = mapper;
            }
            _loggerMock = new Mock<ILogger<RestaurantController>>();

            _context.Database.EnsureCreated();
        }

        [Fact]
        public async Task GetAllRestaurant_ReturnRestaurantCollection()
        {
            /// Arrange
            _context.Restaurants.AddRange(RestaurantMockData.GetAllRestaurants());
            await _context.SaveChangesAsync();

            var sut = new DbDataProvider(_context, _mapperMock, _loggerMock.Object);

            /// Act
            var result = await sut.GetRestaurant();

            /// Assert
            result.Should().HaveCount(RestaurantMockData.GetAllRestaurants().Count);
        }

        [Fact]
        public async Task GetAllMenus_ReturnMenuCollection()
        {
            /// Arrange
            _context.Menus.AddRange(RestaurantMockData.GetAllMenus());
            await _context.SaveChangesAsync();

            var sut = new DbDataProvider(_context, _mapperMock, _loggerMock.Object);

            /// Act
            var result = await sut.GetMenus();

            /// Assert
            result.Should().HaveCount(RestaurantMockData.GetAllMenus().Count);
        }

        [Theory]
        [InlineData("Annapoorna")]
        [InlineData("Aravind Hotel")]
        [InlineData("")]
        public async Task GetFilterRestaurant_ReturnRestaurantsCollection(string stringToBeFiltered)
        {
            /// Arrange
            _context.Restaurants.AddRange(RestaurantMockData.GetFilteredRestaurants(stringToBeFiltered));
            await _context.SaveChangesAsync();

            var sut = new DbDataProvider(_context, _mapperMock, _loggerMock.Object);

            /// Act
            var result = await sut.FilterRestaurant(stringToBeFiltered);

            /// Assert
            result.Should().HaveCount(RestaurantMockData.GetFilteredRestaurants(stringToBeFiltered).Count);
        }

        [Theory]
        [InlineData("Annapoorna")]
        [InlineData("Aravind Hotel")]
        public async Task GetSearchRestaurantAndMenu_ReturnRestaurantAndMenuCollection(string stringToBeFiltered)
        {
            /// Arrange
            _context.Restaurants.AddRange(RestaurantMockData.GetFilteredRestaurants(stringToBeFiltered));
            await _context.SaveChangesAsync();

            var sut = new DbDataProvider(_context, _mapperMock, _loggerMock.Object);

            /// Act
            var result = await sut.SearchMenuAndRestaurant(stringToBeFiltered);
            /// Assert
            result.menu.Should().HaveCount(RestaurantMockData.GetSearchRestaurantAndMenu(stringToBeFiltered).menu.Count);
            result.restaurant.Should().HaveCount(RestaurantMockData.GetSearchRestaurantAndMenu(stringToBeFiltered).restaurant.Count);
        }

        public void Dispose()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }
    }
}