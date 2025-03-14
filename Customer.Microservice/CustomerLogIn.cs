using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Customer.Identity.Microservice.Domain
{
    [Table("CustomerLogin")]
    public class CustomerLogIn
    {
        [Key]
        public int CustomerId { get; set; }

        public string Email { get; set; }

        public string Pass { get; set; }

        public string? sessionToken { get; set; }
        
        public DateTime? LogInTime { get; set; }
    }
}
