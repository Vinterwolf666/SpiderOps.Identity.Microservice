using Customer.Identity.Microservice.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Customer.Identity.Microservice.App
{
    public class CustomerLogInServices : ICustomerLogInServices
    {

        private readonly ICustomerLogInRepository _customerRepository;

        public CustomerLogInServices(ICustomerLogInRepository  r)
        {
            
            _customerRepository = r;

        }

        public Task<List<CustomerLogIn>> AllCustomerLogIns(int id)
        {
            var result = _customerRepository.AllCustomerLogIns(id);

            return result;
        }

        public Task<List<object>> CustomerLogIn(CustomerLogIn c)
        {
            var result = _customerRepository.CustomerLogIn(c);

            return result;
        }


        public async Task<List<object>> ForgotPass(string email)
        {
            var result = await _customerRepository.ForgotPass(email);
            return result;
        }


       
    }
}
