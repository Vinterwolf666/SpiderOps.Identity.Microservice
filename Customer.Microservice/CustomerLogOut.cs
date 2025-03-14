using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Customer.Identity.Microservice.Domain
{
    [Table("CustomerLogout")]
    public class CustomerLogOut
    {
        [Key]
        public int tokenID { get; set; }    

        public int customerId { get; set; }

        public string revokedToken { get; set; }

        public DateTime LogOutTime { get; set; }

    }
}
