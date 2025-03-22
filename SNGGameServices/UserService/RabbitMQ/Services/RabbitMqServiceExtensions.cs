using GetAwaitService.RabbitMQ.Client;
using GetAwaitService.RabbitMQ.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Client;
using System.Threading.Tasks;
using UserService.Consumers.User;

namespace GetAwaitService.RabbitMQ.Extensions
{
    public static class RabbitMqServiceExtensions
    {
        public static async Task AddRabbitMqClient(this IServiceCollection services, RabbitMqOptions rabbitMqOptions)
        {
            var connectionFactory = new ConnectionFactory
            {
                HostName = rabbitMqOptions.HostName,
                UserName = rabbitMqOptions.UserName,
                Password = rabbitMqOptions.Password,
                Port = rabbitMqOptions.Port,
            };

            var connection = await connectionFactory.CreateConnectionAsync();
            var channel = await connection.CreateChannelAsync();

            // Регистрация RabbitMqRpcClient
            services.AddSingleton<IRabbitMqService>(new RabbitMqRpcClient(connection, channel));

            // Регистрация фонового сервиса для закрытия соединения
            services.AddSingleton<IHostedService>(provider =>
                new RabbitMqBackgroundService(connection, channel));

            // Регистрация консьюмера
            services.AddHostedService<UserConsumer>();
        }
    }

    public class RabbitMqOptions
    {
        public string HostName { get; set; } = "localhost";
        public string UserName { get; set; } = "guest";
        public string Password { get; set; } = "guest";
        public int Port { get; set; } = 5672;
    }
}