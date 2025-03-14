using Customer.Identity.Microservice.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Customer.Identity.Microservice.App
{
    public interface ICustomerLogOutRepository 
    {
         Task<string> CustomerLogOut(int id, string token);

        Task<List<CustomerLogOut>> AllCustomerLogOut(int id);
    }
}
