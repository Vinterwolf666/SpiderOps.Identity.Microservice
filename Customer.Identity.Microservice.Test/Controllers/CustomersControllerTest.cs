using Xunit;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Customer.Identity.Microservice.API.Controllers;
using Customer.Identity.Microservice.App;
using Customer.Identity.Microservice.Domain;
using Customer.Identity.Microservice.API.Services;

namespace Customer.Identity.Microservice.Test.Controllers
{
    public class CustomersControllerTest
    {
        private readonly Mock<ICustomersServices> _mockService;
        private readonly CustomersController _controller;
        private readonly Mock<RabbitMQProducer> _mockProducer;

        public CustomersControllerTest()
        {
            _mockService = new Mock<ICustomersServices>();
            _mockProducer = new Mock<RabbitMQProducer>();
            _controller = new CustomersController(_mockService.Object);
        }

        [Fact]
        public void AllCustomers_ReturnsOk_WithList()
        {
            
            var customersList = new List<Customers> { new Customers { ID = 1, EMAIL = "john.doe@example.com", PASS = "12345" } };
            _mockService.Setup(s => s.AllCustomers()).Returns(customersList);

            
            var result = _controller.AllCustomers();

            
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnedList = Assert.IsType<List<Customers>>(okResult.Value);
            Assert.Single(returnedList);
        }

        [Fact]
        public void AllCustomerInfoByID_ReturnsOk_WithCustomer()
        {
           
            int customerId = 1;
            var customer = new List<Customers> { new Customers { ID = customerId, EMAIL = "jane.doe@example.com", PASS = "password" } };
            _mockService.Setup(s => s.AllCustomerByID(customerId)).Returns(customer);

            
            var result = _controller.AllCustomerInfoByID(customerId);

            
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnedCustomer = Assert.IsType<List<Customers>>(okResult.Value);
            Assert.Single(returnedCustomer);
        }

        [Fact]
        public async Task DeleteAnAccount_ReturnsOk_WithMessage()
        {
           
            int customerId = 1;
            string expectedMessage = "Customer deleted successfully";
            _mockService.Setup(s => s.DeleteCustomer(customerId)).ReturnsAsync(expectedMessage);

            
            var result = await _controller.DeleteAnAccount(customerId);

            
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            Assert.Equal(expectedMessage, okResult.Value);
        }

        [Fact]
        public async Task NewCustomer_ReturnsOk_WithMessage()
        {
           
            var newCustomer = new Customers { ID = 2, EMAIL = "alice.smith@example.com", PASS = "securepass" };
            string expectedMessage = "Customer created successfully";

            _mockService.Setup(s => s.NewCustomer(newCustomer)).ReturnsAsync(expectedMessage);
            _mockProducer.Setup(p => p.NotifyAccountCreationStageCompleted()).Returns(Task.CompletedTask);

           
            var result = await _controller.NewCustomer(newCustomer);

            
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            Assert.Equal(expectedMessage, okResult.Value);
        }

        [Fact]
        public async Task UpdateAnAccount_ReturnsOk_WithMessage()
        {
            
            var customerToUpdate = new Customers { ID = 1, EMAIL = "updated.email@example.com", PASS = "newpassword" };
            string expectedMessage = "Customer updated successfully";

            _mockService.Setup(s => s.UpdateCustomer(customerToUpdate)).ReturnsAsync(expectedMessage);

            
            var result = await _controller.UpdateAnAccount(customerToUpdate);

            
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            Assert.Equal(expectedMessage, okResult.Value);
        }
    }
}
