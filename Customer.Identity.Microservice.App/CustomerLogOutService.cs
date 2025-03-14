using Customer.Identity.Microservice.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeZoneConverter;

namespace Customer.Identity.Microservice.App
{
    public class CustomerLogOutService : ICustomerLogOutServices
    {
        private readonly ICustomerLogOutRepository _customerLogInRepository;

        public CustomerLogOutService(ICustomerLogOutRepository customerLogInRepository)
        {

            _customerLogInRepository = customerLogInRepository;

        }

        public Task<List<CustomerLogOut>> AllCustomerLogOut(int id)
        {
            var result = _customerLogInRepository.AllCustomerLogOut(id);

            return result;
        }

        public Task<string> CustomerLogOut(int id, string token)
        {
            var result = _customerLogInRepository.CustomerLogOut(id, token);

            return result;
        }
    }
}
