using Customer.Identity.Microservice.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Customer.Identity.Microservice.App
{
    public interface ICustomerLogInServices
    {
        Task<List<object>> CustomerLogIn(CustomerLogIn c);

        Task<List<CustomerLogIn>> AllCustomerLogIns(int id);

        Task<List<object>> ForgotPass(string email);

       
    }
}
