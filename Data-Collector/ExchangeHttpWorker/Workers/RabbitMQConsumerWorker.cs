using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMqProvider.Models;

namespace ExchangeHttpWorker.Workers;

public class RabbitMqConsumerWorker : IHostedService
    {
        private readonly ILogger<RabbitMqConsumerWorker> _logger;
        private readonly RabbitMqConnectionInformation _connectionInfo;
        
        private readonly IConnection _connection;
        private readonly IModel _channel;
        
        private string? _consumerTag;
        

        public RabbitMqConsumerWorker(IConnectionFactory connectionFactory, ILogger<RabbitMqConsumerWorker> logger, RabbitMqConnectionInformation connectionInfo)
        {
            _logger = logger;
            _connectionInfo = connectionInfo;
            _connection = connectionFactory.CreateConnection();
            _channel = _connection.CreateModel();
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _channel.QueueDeclare(_connectionInfo.QueueName, durable: false, exclusive: false, autoDelete: false, arguments: null);
            _channel.ExchangeDeclare(exchange:_connectionInfo.Exchange , type: "direct", durable: true, autoDelete: false);
            _channel.QueueBind(queue: _connectionInfo.QueueName, exchange: _connectionInfo.Exchange, routingKey: _connectionInfo.RoutingKey);
            
            var consumer = new AsyncEventingBasicConsumer(_channel);
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
            
                return Task.CompletedTask;
            };
            
            _consumerTag = _channel.BasicConsume(_connectionInfo.QueueName, autoAck: true, consumer);

            _logger.LogInformation("RabbitMQ consumer started successfully.");

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _channel.BasicCancel(_consumerTag); 
            _channel.Close();
            _connection.Close();
            
            _logger.LogInformation("RabbitMQ consumer stopped.");
            return Task.CompletedTask;
        }
    }
    