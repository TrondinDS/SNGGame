using GetAwaitService.RabbitMQ.Services.Interfaces;
using System.Text.Json;

namespace UserService.Consumers.User
{
    public class UserConsumer : BackgroundService
    {
        private readonly IRabbitMqService _rabbitMqService;

        public UserConsumer(IRabbitMqService rabbitMqService)
        {
            _rabbitMqService = rabbitMqService;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await _rabbitMqService.ReceiveMessage("testQueue", HandleOrderMessage);
        }

        private void HandleOrderMessage(string message)
        {
            Console.WriteLine(message);
        }
    }
}
