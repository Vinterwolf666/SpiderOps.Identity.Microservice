
using Customer.Identity.Microservice.API.Services;
using Customer.Identity.Microservice.App;
using Customer.Identity.Microservice.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Customer.Identity.Microservice.API.Controllers
{
    [ApiController]
    [Route("Customer.Identity.Microservice.Customers.API/[controller]")]
    public class CustomersController : Controller
    {
        private readonly ICustomersServices _services;
        public CustomersController(ICustomersServices s)
        {
            _services = s;
        }

        [HttpGet]
        [Route("AllCustomers")]
        public ActionResult<List<Customers>> AllCustomers()
        {
            try
            {
                var result = _services.AllCustomers();

                return Ok(result);

            }catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }


        [HttpGet]
        [Route("AllCustomerInfoByID")]
        [Authorize]
        public ActionResult<List<Customers>> AllCustomerInfoByID(int id)
        {

            try
            {

                var result = _services.AllCustomerByID(id);

                return Ok(result);

            }catch (Exception ex)
            {
                return BadRequest(ex.Message);  

            }
        }

        [HttpDelete]
        [Route("deleteAnAccount")]
        [Authorize]
        public async Task<ActionResult<string>> DeleteAnAccount(int id)
        {
            try
            {
                var result = await _services.DeleteCustomer(id);

                return Ok(result);


            }catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("newCustomer")]
        public async Task<object> NewCustomer([FromBody] Customers c)
        {

            try
            {
                var result = await _services.NewCustomer(c);
                var producer = new RabbitMQProducer();
                await producer.NotifyAccountCreationStageCompleted();
                return Ok(result);

            }catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Route("updateAnAccount")]
        [Authorize]
        public async Task<ActionResult<string>> UpdateAnAccount(Customers c)
        {
            try
            {
                var result = await _services.UpdateCustomer(c);

                return Ok(result);


            }catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
