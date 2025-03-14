using BCrypt.Net;
using Customer.Identity.Microservice.App;
using Customer.Identity.Microservice.Domain;
using EasyNetQ;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeZoneConverter;

namespace Customer.Identity.Microservice.Infrastructure
{
    public class CustomerLogInRepository : ICustomerLogInRepository
    {

        private readonly CustomerLogInDbContext _context;

        private readonly CustomerDbContext _dbContext;

        
        private readonly IToken _token;

        public CustomerLogInRepository(CustomerLogInDbContext d, IToken token, CustomerDbContext b)
        {
            _context = d;

           
            _token = token;

            _dbContext = b;
        }

        public async Task<List<CustomerLogIn>> AllCustomerLogIns(int id)
        {
            var customer = await _context.CustomerLogInDomain.Where(a => a.CustomerId == id).ToListAsync();

            return customer;
        }

        public async Task<List<object>> CustomerLogIn(CustomerLogIn c)
        {
            var result = new List<object>();

            
            var customer =  await _dbContext.CustomersDomain
                                         .FirstOrDefaultAsync(a => a.EMAIL == c.Email);

            if (customer == null)
            {
                return result;
            }

            
            var unhashedPass = BCrypt.Net.BCrypt.EnhancedVerify(c.Pass, customer.PASS);

            if (!unhashedPass)
            {
                return result;
            }

            
            var tokens = _token.TokenGeneration(c);

            
            var newLogIn = new CustomerLogIn
            {
                CustomerId = customer.ID,
                Email = customer.EMAIL,
                Pass = customer.PASS, 
                sessionToken = tokens,
                LogInTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TZConvert.GetTimeZoneInfo("America/Bogota"))
            };

            _context.CustomerLogInDomain.Add(newLogIn);
            await _context.SaveChangesAsync();

            return new List<object>
    {
        new
        {
            customerId = customer.ID,
            Email = customer.EMAIL,
            SessionToken = tokens,
            LogInTime = newLogIn.LogInTime
        }
    };
        }



        public async Task<List<object>> ForgotPass(string email)
        {
            var emails =  await _dbContext.CustomersDomain.FirstOrDefaultAsync(a=>a.EMAIL == email);

            if(emails != null)
            {
                return new List<object>
                {
                     new {

                        response = "We will send an email to recover your password, be patient and check your mailbox.",
                        customer_email = email,
                        customer_id = emails.ID

                    }
                };
            }
            else
            {
                return new List<object>
                {
                    new
                    {
                        response = "Please, type a valid email. Try again with one different"
                    }
                };
            }

            
        }

    }
}

