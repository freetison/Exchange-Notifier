using Microsoft.Extensions.Logging;

using RabbitMQ.Client;

namespace RabbitMqProvider.Client
{
    public abstract class RabbitMqClientBase : IDisposable
    {
        private readonly string _loggerExchange;
        private readonly string _loggerQueue;
        private readonly string _queueAndExchangeRoutingKey;

        protected IModel? Channel { get; private set; }
        private IConnection? _connection;
        private readonly ConnectionFactory _connectionFactory;
        private readonly ILogger<RabbitMqClientBase> _logger;

        protected RabbitMqClientBase(ConnectionFactory connectionFactory,  ILogger<RabbitMqClientBase> logger)
        {
            _connectionFactory = connectionFactory;
            _logger = logger;
            _loggerExchange = $"{_connectionFactory.VirtualHost}-moneyRates-direct-exchange";
            _loggerQueue = $"{_connectionFactory.VirtualHost}-moneyRates-queue";
            _queueAndExchangeRoutingKey = $"{_connectionFactory.VirtualHost}-moneyRates-routing-key";
            
            ConnectToRabbitMq();
        }

        private void ConnectToRabbitMq()
        {
            if (_connection == null || _connection.IsOpen == false)
            {
                _connection = _connectionFactory.CreateConnection();
            }

            if (Channel == null || Channel.IsOpen == false)
            {
                Channel = _connection.CreateModel();
                Channel.ExchangeDeclare(
                    exchange: _loggerExchange,
                    type: "direct",
                    durable: true,
                    autoDelete: false);

                Channel.QueueDeclare(
                    queue: _loggerQueue,
                    durable: false,
                    exclusive: false,
                    autoDelete: false);

                Channel.QueueBind(
                    queue: _loggerQueue,
                    exchange: _loggerExchange,
                    routingKey: _queueAndExchangeRoutingKey);
            }
        }

        public void Dispose()
        {
            try
            {
                Channel?.Close();
                Channel?.Dispose();
                Channel = null;

                _connection?.Close();
                _connection?.Dispose();
                _connection = null;
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex, "Cannot dispose RabbitMQ channel or connection");
            }
        }
    }
}
