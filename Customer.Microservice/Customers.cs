using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Customer.Identity.Microservice.Domain
{
    [Table("Customers")]
    public class Customers
    {
        [Key]
        public int ID { get; set; }

        public string EMAIL { get; set; }


        public string PASS { get; set; }
    }
}
