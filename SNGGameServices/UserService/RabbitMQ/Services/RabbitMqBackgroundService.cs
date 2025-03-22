using Microsoft.Extensions.Hosting;
using RabbitMQ.Client;

namespace GetAwaitService.RabbitMQ.Client
{
    public class RabbitMqBackgroundService : IHostedService
    {
        private readonly IConnection _connection;
        private readonly IChannel _channel;

        public RabbitMqBackgroundService(IConnection connection, IChannel channel)
        {
            _connection = connection;
            _channel = channel;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            // Ничего не делаем при запуске
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            // Закрываем канал и соединение при завершении работы приложения
            _channel?.CloseAsync();
            _connection?.CloseAsync();
            return Task.CompletedTask;
        }
    }
}