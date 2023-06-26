using AutoMapper;
using FoodOrderApi.Controllers;
using FoodOrderApi.DataProvider;
using FoodOrderApi.Model.Domain;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System.Text;
using System.Text.Json;

namespace UnitTests
{
    public class AdminControllerTests
    {
        private readonly Mock<IDataProvider> _dataProvider;
        private readonly Mock<ILogger<AdminController>> _logger;
        private readonly Mock<IMapper> _mapper;

        public AdminControllerTests()
        {
            _dataProvider = new Mock<IDataProvider>();
            _logger = new Mock<ILogger<AdminController>>();
            _mapper = new Mock<IMapper>();
        }

        [Fact]
        public void DeleteMenu_NotExistingGuidPassed_ReturnsBadRequestResponse()
        {
            // Arrange
            var notExistingGuid = Guid.NewGuid();
            _dataProvider.Setup(x => x.DeleteMenu(notExistingGuid)).ReturnsAsync(false);
            var adminController = new AdminController(_dataProvider.Object, _mapper.Object, _logger.Object);

            // Act
            var badResponse = adminController.DeleteMenu(notExistingGuid);

            // Assert
            Assert.IsType<BadRequestObjectResult>(badResponse.Result);
        }

        [Theory]
        [InlineData("E0194510-6AD1-4AC2-BF31-E572CAA09BA1")]
        [InlineData("983FC832-5371-4BDC-AF27-BD14F5998F44")]
        [InlineData("AE527A12-BE03-4AC1-8206-7345A1A90BF6")]
        public void DeleteMenu_ExistingGuidPassed_ReturnsOkResponse(Guid MenuID)
        {
            // Arrange
             _dataProvider.Setup(x => x.DeleteMenu(MenuID)).ReturnsAsync(true);
            var adminController = new AdminController(_dataProvider.Object, _mapper.Object, _logger.Object);

            // Act
            var okResponse = adminController.DeleteMenu(MenuID);

            // Assert
            Assert.IsType<OkObjectResult>(okResponse.Result);
        }

        [Fact]
        public void OrderDelivered_NotExistingOrderIdPassed_ReturnsNotFoundResponse()
        {
            // Arrange
            var notExistingGuid = Guid.NewGuid();
            _dataProvider.Setup(x => x.OrderDelivered(notExistingGuid)).ReturnsAsync(false);
            var adminController = new AdminController(_dataProvider.Object, _mapper.Object, _logger.Object);

            // Act
            var NotFoundResponse = adminController.OrderDelivered(notExistingGuid);

            // Assert
            Assert.IsType<NotFoundObjectResult>(NotFoundResponse.Result);
        }

        [Theory]
        [InlineData("A6CA1D06-38EE-4C77-8B0B-3190D220DDB2")]
        [InlineData("A6CA1D06-38EE-4C77-8B0B-3190D220DDB3")]
        public void OrderDelivered_ExistingOrderIdPassed_ReturnsOkResponse(Guid OrderID)
        {
            // Arrange
            _dataProvider.Setup(x => x.OrderDelivered(OrderID)).ReturnsAsync(true);
            var adminController = new AdminController(_dataProvider.Object, _mapper.Object, _logger.Object);

            // Act
            var okResponse = adminController.OrderDelivered(OrderID);

            // Assert
            Assert.IsType<OkObjectResult>(okResponse.Result);
        }

        [Theory]
        [InlineData("BAAD586A-ACCF-4433-98F1-2F861E683354", "E0194510-6AD1-4AC2-BF32-E572CAA09BA1", 20 )]
        public void Discount_NotExistingRestaurantPassed_ReturnsNotFoundResponse(Guid restaurantID, Guid productID, double discount)
        {
            // Arrange
            _dataProvider.Setup(x => x.Discount(restaurantID, productID, discount)).ReturnsAsync(false);
            var adminController = new AdminController(_dataProvider.Object, _mapper.Object, _logger.Object);

            // Act
            var NotFoundResponse = adminController.Discount(restaurantID, productID, discount);

            // Assert
            Assert.IsType<NotFoundObjectResult>(NotFoundResponse.Result);
        }

        [Theory]
        [InlineData("BAAD586A-ACCF-4433-98F0-2F861E683354", "E0194510-6AD1-4AC2-BF31-E572CAA09BA1", 30)]
        public void Discount_ExistingRestaurantPassed_ReturnsOkResponse(Guid restaurantID, Guid productID, double discount)
        {
            // Arrange
            _dataProvider.Setup(x => x.Discount(restaurantID, productID, discount)).ReturnsAsync(true);
            var adminController = new AdminController(_dataProvider.Object, _mapper.Object, _logger.Object);

            // Act
            var okResponse = adminController.Discount(restaurantID, productID, discount);

            // Assert
            Assert.IsType<OkObjectResult>(okResponse.Result);
        }

        [Fact]
        public void AddMenuToTheRestaurant_NotExistingRestaurantIdPassed_ReturnsBadRequestResponse()
        {
            // Arrange
            var notExistingGuid = Guid.NewGuid();
            JsonPatchDocument<Menu> jsonPatchDocument = new JsonPatchDocument<Menu>();
            _dataProvider.Setup(x => x.PatchMenuItems(notExistingGuid, jsonPatchDocument));
            var adminController = new AdminController(_dataProvider.Object, _mapper.Object, _logger.Object);

            // Act
            var badResponse = adminController.AddMenuToTheRestaurant(notExistingGuid, jsonPatchDocument);

            // Assert
            Assert.IsType<BadRequestResult>(badResponse.Result);
        }
    }
}