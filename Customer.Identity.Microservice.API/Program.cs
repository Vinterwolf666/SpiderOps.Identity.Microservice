
using Customer.Identity.Microservice.API.Services;
using Customer.Identity.Microservice.App;
using Customer.Identity.Microservice.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Customer.Identity.Microservice.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

          

            builder.Services.AddControllers();
            
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var configuration = builder.Configuration;

            builder.Services.AddDbContext<CustomerLogInDbContext>(opt => opt.UseSqlServer(configuration.GetConnectionString("Value"), b => b.MigrationsAssembly("Customer.Identity.Microservice.API")));
            builder.Services.AddDbContext<CustomerLogOutDbContext>(opt => opt.UseSqlServer(configuration.GetConnectionString("Value"), b => b.MigrationsAssembly("Customer.Identity.Microservice.API")));
            builder.Services.AddDbContext<CustomerDbContext>(opt => opt.UseSqlServer(configuration.GetConnectionString("Value"), b => b.MigrationsAssembly("Customer.Identity.Microservice.API")));

            builder.Services.AddScoped<ICustomerLogInRepository, CustomerLogInRepository>();
            builder.Services.AddScoped<ICustomerLogInServices, CustomerLogInServices>();
            builder.Services.AddScoped<IToken, TokenService>();
            builder.Services.AddScoped<ICustomerLogOutRepository, CustomerLogOutRepository>();
            builder.Services.AddScoped<ICustomerLogOutServices, CustomerLogOutService>();
            builder.Services.AddScoped<ICustomersServices, CustomersService>();
            builder.Services.AddScoped<ICustomersRepository, CustomersRepository>();
            builder.Services.AddScoped<RabbitMQRecoveryPassProducer>();



            builder.Services.AddCors(options =>
            {

                options.AddPolicy("nuevaPolitica", app =>
                {

                    app.AllowAnyOrigin();
                    app.AllowAnyHeader();
                    app.AllowAnyMethod();
                });



            });


            var app = builder.Build();

           

            
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseCors("nuevaPolitica");

           app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}