using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Customer.Identity.Microservice.API.Controllers;
using Customer.Identity.Microservice.App;
using Customer.Identity.Microservice.Domain;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;


namespace Customer.Identity.Microservice.Test.Controllers
{
    public class CustomerLogOutControllerTest
    {
        
            private readonly Mock<ICustomerLogOutServices> _mockService;
            private readonly CustomerLogOutController _controller;

            public CustomerLogOutControllerTest()
            {
                _mockService = new Mock<ICustomerLogOutServices>();
                _controller = new CustomerLogOutController(_mockService.Object);
            }

        [Fact]
        public async Task CustomersLogOut_ReturnsOk_WithResult()
        {
            // Arrange
            int userId = 1;
            string token = "test-token";
            string expectedResponse = "User logged out successfully";

            _mockService.Setup(s => s.CustomerLogOut(userId, token))
                        .ReturnsAsync(expectedResponse);

            // Act
            var result = await _controller.CustomersLogOut(userId, token);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var responseData = Assert.IsType<Dictionary<string, object>>(
                okResult.Value.GetType().GetProperties().ToDictionary(p => p.Name, p => p.GetValue(okResult.Value))
            );

            Assert.Equal(expectedResponse, responseData["result"]);
        }


        [Fact]
            public async Task CustomersLogOut_ReturnsBadRequest_OnException()
            {
                
                int userId = 1;
                string token = "test-token";

                _mockService.Setup(s => s.CustomerLogOut(userId, token))
                            .ThrowsAsync(new Exception("An error occurred"));

                
                var result = await _controller.CustomersLogOut(userId, token);

                
                var badRequestResult = Assert.IsType<BadRequestObjectResult>(result.Result);
                Assert.Equal("An error occurred", badRequestResult.Value);
            }

            [Fact]
            public async Task AllCustomersLogOuts_ReturnsOk_WithListOfLogouts()
            {
                
                int userId = 1;
                var expectedLogouts = new List<CustomerLogOut>
            {
                new CustomerLogOut { tokenID = 1, customerId = userId, LogOutTime = DateTime.UtcNow },
                new CustomerLogOut { tokenID = 2, customerId = userId, LogOutTime = DateTime.UtcNow.AddMinutes(-30) }
            };

                _mockService.Setup(s => s.AllCustomerLogOut(userId))
                            .ReturnsAsync(expectedLogouts);

                
                var result = await _controller.AllCustomersLogOuts(userId);

                
                var okResult = Assert.IsType<OkObjectResult>(result.Result);
                var actualList = Assert.IsType<List<CustomerLogOut>>(okResult.Value);
                Assert.Equal(expectedLogouts.Count, actualList.Count);
            }

            [Fact]
            public async Task AllCustomersLogOuts_ReturnsBadRequest_OnException()
            {
                // Arrange
                int userId = 1;

                _mockService.Setup(s => s.AllCustomerLogOut(userId))
                            .ThrowsAsync(new Exception("An error occurred"));

                // Act
                var result = await _controller.AllCustomersLogOuts(userId);

                // Assert
                var badRequestResult = Assert.IsType<BadRequestObjectResult>(result.Result);
                Assert.Equal("An error occurred", badRequestResult.Value);
            }
        
    }
}
