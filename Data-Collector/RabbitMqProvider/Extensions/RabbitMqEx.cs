using RabbitMQ.Client;
using RabbitMqProvider.Models;

namespace RabbitMqProvider.Extensions;

public static class RabbitMqEx
{
    public static IModel? CreateChannel(this IConnection connection, string? queueName, RabbitMqQueueParams @params)
    {
        IModel? channel = connection.CreateModel();
        channel.QueueDeclare(queue: queueName, @params.Durable, @params.Exclusive, @params.AutoDelete, @params.Arguments);

        return channel;
    }

    public static IBasicProperties? AddProperties(this IModel? channel,  byte deliveryMode = 1, string? appId = null)
    {
        IBasicProperties? properties = channel?.CreateBasicProperties();
        if (properties != null)
        {
            properties.ContentType = "application/json";
            properties.DeliveryMode = deliveryMode; // Doesn't persist to disk
            properties.Timestamp = new AmqpTimestamp(DateTimeOffset.UtcNow.ToUnixTimeSeconds());
            properties.CorrelationId = Guid.NewGuid().ToString();
            properties.AppId = appId;
        }
        
        return properties;
    }
    
}