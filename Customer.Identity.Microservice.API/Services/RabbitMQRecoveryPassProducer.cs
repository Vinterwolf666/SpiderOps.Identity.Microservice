using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace Customer.Identity.Microservice.API.Services
{
    public class RabbitMQRecoveryPassProducer
    {
        private const string QueueName = "recovery_password_stage";
        private readonly ConnectionFactory _factory;

        public RabbitMQRecoveryPassProducer()
        {
            _factory = new ConnectionFactory()
            {
                HostName = "35.202.158.223",
                UserName = "vinterwolf",
                Password = "vinterland"
            };
        }

        public virtual async Task NotifyRecoveryPasswordRequest(string email)
        {
            await using var connection = await _factory.CreateConnectionAsync();
            await using var channel = await connection.CreateChannelAsync();

            await channel.QueueDeclareAsync(
                queue: QueueName,
                durable: true,
                exclusive: false,
                autoDelete: false,
                arguments: null
            );

            var messageObj = new
            {
                
                email = email,
                account_creation_stage = 2
            };

            var message = JsonSerializer.Serialize(messageObj);
            var body = Encoding.UTF8.GetBytes(message);

            var properties = new BasicProperties();
            await channel.BasicPublishAsync(
                exchange: "",
                routingKey: QueueName,
                mandatory: false,
                basicProperties: properties,
                body: body
            );

            Console.WriteLine($"[x] Sent: {message}");
        }
    }
}

