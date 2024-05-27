using System.Text;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMqProvider.Extensions;
using RabbitMqProvider.Models;

namespace RabbitMqProvider.Connection;

public class RabbitMqClientProvider : IRabbitMqClientProvider
{
    private readonly ILogger<RabbitMqClientProvider> _logger;
    private readonly IConnectionFactory _factory;
    private readonly RabbitMqConnectionInformation _connectionInfo;

    public RabbitMqClientProvider(ILogger<RabbitMqClientProvider> logger, IConnectionFactory factory, RabbitMqConnectionInformation connectionInfo)
    {
        _logger = logger;
        _factory = factory;
        _connectionInfo = connectionInfo;
    }

    public void PublishMessage(string message, string exchange, string queueName, string routingKey)
    {
        using var connection = _factory.CreateConnection();
        using IModel? channel = connection.CreateChannel(_connectionInfo.QueueName, new RabbitMqQueueParams());
        var properties = channel?.AddProperties();
        var body = Encoding.UTF8.GetBytes(message);
        channel.BasicPublish(exchange: exchange, routingKey: routingKey, basicProperties: properties, body: body);
        _logger.LogInformation($"Message was sent to the queue {_connectionInfo.QueueName}");
    }

    public void PublishMessage<T>(T message)
    {
        try
        {
            using var connection = _factory.CreateConnection();
            using IModel? channel = connection.CreateChannel(_connectionInfo.QueueName, new RabbitMqQueueParams());
            var properties = channel?.AddProperties();
            var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(message));
            
            channel!.BasicReturn += (sender, args) =>
            {
                _logger.LogInformation($"Message was not delivered: {args.BasicProperties.MessageId}");
            };
            
            channel.BasicPublish(exchange: _connectionInfo.Exchange, routingKey: _connectionInfo.RoutingKey, body: body, basicProperties: properties);
            _logger.LogInformation($"Message was sent to the queue {_connectionInfo.QueueName}");
        }
        catch (Exception ex)
        {
            _logger.LogCritical(ex, "Error while publishing");
        }

    }

    public void ReadMessages(string queueName, Action<string> processMessage)
    {
        using var connection = _factory.CreateConnection();
        using IModel? channel = connection.CreateChannel(_connectionInfo.QueueName, new RabbitMqQueueParams());

        var consumer = new EventingBasicConsumer(channel);
        consumer.Received += (sender, args) =>
        {
            try
            {
                var message = Encoding.UTF8.GetString(args.Body.ToArray());
                Console.WriteLine($"Received message: {message}");
                    
                // Process the message here (you can make this part asynchronous).
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error processing message");
            }

        };

        channel.BasicConsume(_connectionInfo.QueueName, autoAck: true, consumer);
        
        
        // var consumer = new EventingBasicConsumer(channel);
        // consumer.Received += (model, ea) =>
        // {
        //     var body = ea.Body.ToArray();
        //     var message = Encoding.UTF8.GetString(body);
        //     processMessage(message);
        // };
        // channel.BasicConsume(queue: queueName, autoAck: true, consumer: consumer);

    }
    
    
}