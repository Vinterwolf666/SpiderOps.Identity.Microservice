using Customer.Identity.Microservice.App;
using Customer.Identity.Microservice.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeZoneConverter;

namespace Customer.Identity.Microservice.Infrastructure
{
    public class CustomerLogOutRepository : ICustomerLogOutRepository
    {

        private readonly CustomerLogOutDbContext _context;
        public CustomerLogOutRepository(CustomerLogOutDbContext context)
        {

            _context = context;

        }

        public async Task<List<CustomerLogOut>> AllCustomerLogOut(int id)
        {
            var customer = await _context.CustomerLogOutDomain.Where(a => a.customerId == id).ToListAsync();

            return customer;
        }

        public async Task<string> CustomerLogOut(int id, string token)
        {

            var dateTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TZConvert.GetTimeZoneInfo("America/Bogota"));

             _context.CustomerLogOutDomain.Add(new CustomerLogOut
             {

                 customerId = id,
                 revokedToken = token,
                 LogOutTime = dateTime,


             });

             await _context.SaveChangesAsync();

            return "Thanks, for using this platform, comeback soon !!!";

        }
    }
}
