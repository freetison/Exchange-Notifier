
using RabbitMQ.Client;

namespace RabbitMqProvider.Client
{
    public abstract class RabbitMqClientBase : IDisposable
    {
        const  string Exchange = "moneyRates-direct-exchange";
        const string Queue = "moneyRates-queue";
        const string RoutingKey = "moneyRates-routing-key";

        protected IModel? Channel { get; private set; }
        private IConnection? _connection;
        private readonly ConnectionFactory _connectionFactory;

        protected RabbitMqClientBase(ConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
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
                    exchange: Exchange,
                    type: "direct",
                    durable: true,
                    autoDelete: false);

                Channel.QueueDeclare(
                    queue: Queue,
                    durable: false,
                    exclusive: false,
                    autoDelete: false);

                Channel.QueueBind(
                    queue: Queue,
                    exchange: Exchange,
                    routingKey: RoutingKey);
            }
        }

        public void Dispose()
        {
                Channel?.Close();
                Channel?.Dispose();
                Channel = null;

                _connection?.Close();
                _connection?.Dispose();
                _connection = null;
        }
    }
}
