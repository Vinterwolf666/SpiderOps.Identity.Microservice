using Customer.Identity.Microservice.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Customer.Identity.Microservice.App
{
    public interface IToken
    {
        string TokenGeneration(CustomerLogIn c);
    }
}
