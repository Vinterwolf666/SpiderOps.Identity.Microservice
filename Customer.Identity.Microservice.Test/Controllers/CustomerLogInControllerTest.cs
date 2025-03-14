using Customer.Identity.Microservice.API.Controllers;
using Customer.Identity.Microservice.API.Services;
using Customer.Identity.Microservice.App;
using Customer.Identity.Microservice.Domain;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Customer.Identity.Microservice.Test.Controllers
{
    public class CustomerLogInControllerTest
    {
        private readonly Mock<ICustomerLogInServices> _mockService;
        private readonly Mock<RabbitMQRecoveryPassProducer> _mockRabbitMQ;
        private readonly CustomerLogInController _controller;

        public CustomerLogInControllerTest()
        {
            _mockService = new Mock<ICustomerLogInServices>();
            _mockRabbitMQ = new Mock<RabbitMQRecoveryPassProducer>();
            _controller = new CustomerLogInController(_mockService.Object, _mockRabbitMQ.Object);
        }

        [Fact]
        public async Task CustomersLogIn_ReturnsOk_WithListOfObjects()
        {
            var loginRequest = new CustomerLogIn { Email = "vinterwolf666@gmail.com", Pass = "vinterland" };
            var expectedResponse = new List<object> { "Login Successful" };
            _mockService.Setup(s => s.CustomerLogIn(loginRequest)).ReturnsAsync(expectedResponse);

            var actionResult = await _controller.CustomersLogIn(loginRequest);
            var okResult = actionResult.Result as OkObjectResult;

            Assert.NotNull(okResult);
            Assert.Equal(expectedResponse, okResult.Value);
        }

        [Fact]
        public async Task CustomersLogIn_ReturnsBadRequest_OnException()
        {
            var loginRequest = new CustomerLogIn { Email = "vinterwolf666@gmail.com", Pass = "vinterland" };
            _mockService.Setup(s => s.CustomerLogIn(loginRequest)).ThrowsAsync(new Exception("Login Failed"));

            var actionResult = await _controller.CustomersLogIn(loginRequest);
            var badRequestResult = actionResult.Result as BadRequestObjectResult;

            Assert.NotNull(badRequestResult);
            Assert.Equal("Login Failed", badRequestResult.Value);
        }

        [Fact]
        public async Task AllCustomersLogIns_ReturnsOk_WithListOfCustomerLogins()
        {
            int customerId = 1;
            var expectedLogins = new List<CustomerLogIn> { new CustomerLogIn { Email = "vinterwolf666@gmail.com" } };
            _mockService.Setup(s => s.AllCustomerLogIns(customerId)).ReturnsAsync(expectedLogins);

            var actionResult = await _controller.AllCustomersLogIns(customerId);
            var okResult = actionResult.Result as OkObjectResult;

            Assert.NotNull(okResult);
            Assert.Equal(expectedLogins, okResult.Value);
        }

        [Fact]
        public async Task ForgetPassword_ReturnsOk_WithListOfObjects()
        {
            string email = "vinterwolf666@gmail.com";
            var expectedResponse = new List<object> { "Reset Link Sent" };
            _mockService.Setup(s => s.ForgotPass(email)).ReturnsAsync(expectedResponse);
            _mockRabbitMQ.Setup(r => r.NotifyRecoveryPasswordRequest(email)).Returns(Task.CompletedTask);

            var actionResult = await _controller.ForgetPassword(email);
            var okResult = actionResult.Result as OkObjectResult;

            Assert.NotNull(okResult);
            Assert.Equal(expectedResponse, okResult.Value);
        }

        [Fact]
        public async Task ForgetPassword_ReturnsBadRequest_OnException()
        {
            string email = "vinterwolf666@gmail.com";
            _mockService.Setup(s => s.ForgotPass(email)).ThrowsAsync(new Exception("Error sending reset link"));

            var actionResult = await _controller.ForgetPassword(email);
            var badRequestResult = actionResult.Result as BadRequestObjectResult;

            Assert.NotNull(badRequestResult);
            Assert.Equal("Error sending reset link", badRequestResult.Value);
        }
    }


}

