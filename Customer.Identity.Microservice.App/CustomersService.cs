using Customer.Identity.Microservice.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Customer.Identity.Microservice.App
{
    public class CustomersService : ICustomersServices
    {
        private readonly ICustomersRepository _repository;

        public CustomersService(ICustomersRepository repository)
        {

            _repository = repository;

        }
        public List<Customers> AllCustomerByID(int id)
        {
            var result = _repository.AllCustomerByID(id);

            return result;
        }

        public List<Customers> AllCustomers()
        {
            var result = _repository.AllCustomers();
            return result;
        }

        public Task<string> DeleteCustomer(int id)
        {
            var result = _repository.DeleteCustomer(id);

            return result;
        }
        public Task<string> NewCustomer(Customers c)
        {
            var result = _repository.NewCustomer(c);

            return result;
        }

        public Task<string> UpdateCustomer(Customers c)
        {
            var result = _repository.UpdateCustomer(c);

            return result;
        }
    }
}
