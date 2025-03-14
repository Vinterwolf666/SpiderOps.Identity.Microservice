using Customer.Identity.Microservice.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Customer.Identity.Microservice.Infrastructure
{
    public class CustomerLogOutDbContext : DbContext
    {
        public CustomerLogOutDbContext(DbContextOptions<CustomerLogOutDbContext> options)
            :base(options)
        {
            
        }



       public DbSet<CustomerLogOut> CustomerLogOutDomain { get; set; }
    }
}
