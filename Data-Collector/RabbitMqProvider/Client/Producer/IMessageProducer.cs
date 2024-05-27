namespace RabbitMqProvider.Client.Producer;

public interface IMessageProducer
{
    Task SendMessageAsync<T>(T message, string toExchange, string toQueue);
}
