using GetAwaitService.RabbitMQ.Services.Interfaces;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace GetAwaitService.RabbitMQ.Client
{
    /// <summary>
    /// Клиент для работы с RabbitMQ, поддерживающий асинхронную отправку и получение сообщений.
    /// </summary>
    public class RabbitMqRpcClient : IRabbitMqService
    {
        private readonly IConnection _connection;
        private readonly IChannel _channel;

        /// <summary>
        /// Инициализирует новый экземпляр класса RabbitMqRpcClient.
        /// </summary>
        /// <param name="connection">Активное подключение к RabbitMQ.</param>
        /// <param name="channel">Канал для взаимодействия с очередями RabbitMQ.</param>
        public RabbitMqRpcClient(IConnection connection, IChannel channel)
        {
            _connection = connection;
            _channel = channel;
        }

        /// <summary>
        /// Асинхронно отправляет сообщение в указанную очередь RabbitMQ.
        /// </summary>
        /// <param name="queueName">Имя очереди, в которую будет отправлено сообщение.</param>
        /// <param name="message">Сообщение для отправки.</param>
        /// <remarks>
        /// Если очередь не существует, она будет создана с указанными параметрами:
        /// - durable: true (сообщения сохраняются на диске),
        /// - exclusive: false (очередь доступна для других подключений),
        /// - autoDelete: false (очередь не удаляется при отключении потребителей).
        /// </remarks>
        public async Task SendMessageAsync(string queueName, string message)
        {
            await _channel.QueueDeclareAsync(
                queue: queueName, 
                durable: true, 
                exclusive: false, 
                autoDelete: false, 
                arguments: null
            );

            var body = Encoding.UTF8.GetBytes(message);

            await _channel.BasicPublishAsync(
                exchange: "", 
                routingKey: queueName, 
                body: body
            );
        }

        /// <summary>
        /// Асинхронно подписывается на получение сообщений из указанной очереди RabbitMQ.
        /// </summary>
        /// <param name="queueName">Имя очереди, из которой будут получаться сообщения.</param>
        /// <param name="onMessageReceived">Callback, вызываемый при получении нового сообщения.</param>
        /// <remarks>
        /// При получении сообщения оно автоматически подтверждается (autoAck = true).
        /// Если очередь не существует, она будет создана с указанными параметрами:
        /// - durable: true (сообщения сохраняются на диске),
        /// - exclusive: false (очередь доступна для других подключений),
        /// - autoDelete: false (очередь не удаляется при отключении потребителей).
        /// </remarks>
        public async Task ReceiveMessage(string queueName, Action<string> onMessageReceived)
        {
            await _channel.QueueDeclareAsync(
                queue: queueName,
                durable: true,
                exclusive: false,
                autoDelete: false,
                arguments: null
            );

            var consumer = new AsyncEventingBasicConsumer(_channel);

            consumer.ReceivedAsync += async (model, ea) =>
            {
                try
                {
                    var body = ea.Body.ToArray();
                    var message = Encoding.UTF8.GetString(body);

                    // Вызываем callback для обработки сообщения
                    onMessageReceived?.Invoke(message);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error processing message: {ex.Message}");
                }

                // Возвращаем завершенную задачу
                await Task.CompletedTask;
            };

            await _channel.BasicConsumeAsync(
                queue: queueName,
                autoAck: true,
                consumer: consumer
            );
        }


        /// <summary>
        /// Освобождает ресурсы, связанные с подключением и каналом RabbitMQ.
        /// </summary>
        /// <remarks>
        /// Закрывает канал и подключение к RabbitMQ асинхронно.
        /// </remarks>
        public void Dispose()
        {
            _channel?.CloseAsync();
            _connection?.CloseAsync();
        }
    }

}
