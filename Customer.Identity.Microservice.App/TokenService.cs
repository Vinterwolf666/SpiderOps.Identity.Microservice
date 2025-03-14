using Customer.Identity.Microservice.Domain;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Customer.Identity.Microservice.App
{
    public class TokenService : IToken
    {

        private readonly IConfiguration _config;

        public TokenService(IConfiguration c)
        {
            _config = c;
        }


        public string TokenGeneration(CustomerLogIn c)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
            new Claim(ClaimTypes.NameIdentifier, c.CustomerId.ToString()),
            new Claim(ClaimTypes.Email, c.Email),
            new Claim("role", "Customer") 
        };

            var token = new JwtSecurityToken(
                _config["Jwt:Issuer"],
                _config["Jwt:Audience"],
                claims,
                expires: DateTime.UtcNow.AddHours(2),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }

}

