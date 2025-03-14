using Customer.Identity.Microservice.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Customer.Identity.Microservice.App
{
     public interface ICustomersServices
    {
        List<Customers> AllCustomers();

        List<Customers> AllCustomerByID(int id);

        Task<string> NewCustomer(Customers c);

        Task<string> DeleteCustomer(int id);

        Task<string> UpdateCustomer(Customers c);

    }
}
