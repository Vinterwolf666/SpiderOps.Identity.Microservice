using Customer.Identity.Microservice.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Customer.Identity.Microservice.Infrastructure
{
    public class CustomerLogInDbContext : DbContext
    {
        public CustomerLogInDbContext(DbContextOptions<CustomerLogInDbContext> options)
            :base(options)
        {
            

        }

       public DbSet<CustomerLogIn> CustomerLogInDomain { get; set; }

    }
}
