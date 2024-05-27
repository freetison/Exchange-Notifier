namespace RabbitMqProvider.Connection;

public interface IRabbitMqClientProvider
{
    void PublishMessage(string message, string exchange, string queueName, string routingKey);
    void PublishMessage<T>(T message);
    void ReadMessages(string queueName, Action<string> processMessage);
}

