namespace GetAwaitService.RabbitMQ.Services.Interfaces;
public interface IRabbitMqService : IDisposable
{
    Task SendMessageAsync(string queueName, string message);
    Task ReceiveMessage(string queueName, Action<string> onMessageReceived);
}