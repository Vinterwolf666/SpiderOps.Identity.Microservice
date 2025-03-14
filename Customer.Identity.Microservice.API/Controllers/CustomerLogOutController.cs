using Customer.Identity.Microservice.App;
using Customer.Identity.Microservice.Domain;
using Microsoft.AspNetCore.Mvc;
using TimeZoneConverter;

namespace Customer.Identity.Microservice.API.Controllers
{
    [ApiController]
    [Route("CustomerLogOut/[controller]")]
    public class CustomerLogOutController : Controller
    {
        private readonly ICustomerLogOutServices _services;

        public CustomerLogOutController(ICustomerLogOutServices s)
        {
            
            _services = s;
        }

        [HttpPost]
        [Route("LogOut")]
        public async Task<ActionResult<string>> CustomersLogOut([FromRoute] int id, string token)
        {
            try
            {
          
                var results = await _services.CustomerLogOut(id, token);

                return Ok(new { result = results});


            }catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }

        }


        [HttpPost]
        [Route("AllLogOutsByCustomerId")]
        public async Task<ActionResult<List<CustomerLogOut>>> AllCustomersLogOuts(int id)
        {
            try
            {

                var results = await _services.AllCustomerLogOut(id);

                return Ok(results);



            }catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
