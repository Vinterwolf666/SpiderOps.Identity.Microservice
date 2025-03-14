using Customer.Identity.Microservice.API.Services;
using Customer.Identity.Microservice.App;
using Customer.Identity.Microservice.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Customer.Identity.Microservice.API.Controllers
{
    [ApiController]
    [Route("CustomerLogIn.API/[controller]")]
    public class CustomerLogInController : Controller
    {
        private readonly ICustomerLogInServices _services;
        private readonly RabbitMQRecoveryPassProducer _rabbitMQProducer;
        public CustomerLogInController(ICustomerLogInServices s, RabbitMQRecoveryPassProducer _rabbit)
        {
            
            _services = s;
            _rabbitMQProducer = _rabbit;
        }

        [HttpPost]
        [Route("LogIn")]
        public async Task<ActionResult<List<object>>> CustomersLogIn(CustomerLogIn c)
        {

            try
            {

                var result = await _services.CustomerLogIn(c);

                return Ok(result);


            }catch (Exception ex)
            {

                return BadRequest(ex.Message);

            }
        }


        [HttpPost]
        [Route("AllLogInsByCustomerId")]
        [Authorize]
        public async Task<ActionResult<List<CustomerLogIn>>> AllCustomersLogIns(int id)
        {
            try
            {

                var result = await _services.AllCustomerLogIns(id);

                return Ok(result);


            }catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpPost]
        [Route("ForgetPassword")]
        public async Task<ActionResult<List<object>>> ForgetPassword(string email)
        {
            try
            {

                var result = await _services.ForgotPass(email);
                await _rabbitMQProducer.NotifyRecoveryPasswordRequest(email);

                return Ok(result);


            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }




    }
}
