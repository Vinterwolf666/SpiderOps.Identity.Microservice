using Customer.Identity.Microservice.App;
using Customer.Identity.Microservice.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Customer.Identity.Microservice.Infrastructure
{
    public class CustomersRepository : ICustomersRepository
    {
        private readonly CustomerDbContext _context;

        public CustomersRepository(CustomerDbContext c)
        {
            _context = c;
        }
        public List<Customers> AllCustomerByID(int id)
        {
            var result = _context.CustomersDomain.Where(a => a.ID == id).ToList();

            return result;
        }

        public List<Customers> AllCustomers()
        {
            var result = _context.CustomersDomain.ToList();

            return result;
        }

        public async Task<string> DeleteCustomer(int id)
        {
            var result = _context.CustomersDomain.FirstOrDefault(a => a.ID == id);

            if (result != null)
            {

                _context.CustomersDomain.Remove(result);

                await _context.SaveChangesAsync();

                return "Account removed successfully";

            }
            else
            {
                return "Invalid ID";
            }

        }

        public async Task<object> NewCustomer(Customers c)
        {

            var validate_email = _context.CustomersDomain.Where(a => a.EMAIL == c.EMAIL).FirstOrDefault();


            if (validate_email == null)
            {
                c.PASS = BCrypt.Net.BCrypt.EnhancedHashPassword(c.PASS);

                _context.CustomersDomain.Add(c);

                await _context.SaveChangesAsync();

                return new
                {

                    response = "Account created successfully",

                    customer_id = c.ID
                };

            }
            else
            {
                return new
                {
                    response = "The email is already in use, try another"
                };

            }
        }

        public async Task<string> UpdateCustomer(Customers c)
        {
            var result = _context.CustomersDomain.FirstOrDefault(a=>a.ID == c.ID);

            if(result != null)
            {

                result.EMAIL = c.EMAIL;
                result.PASS = BCrypt.Net.BCrypt.HashPassword(c.PASS);

                await _context.SaveChangesAsync();

                return "Account updated sucessfully";
            }
            else
            {
                return "Error while updating the account";
            }
        }
    }
}
