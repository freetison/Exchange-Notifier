using Microsoft.Extensions.Logging;

using RabbitMQ.Client;

using RabbitMqProvider.Client;
using RabbitMqProvider.Client.Producer;
using RabbitMqProvider.Models;

namespace ExchangeHttpWorker.RabbitMq.Producer;

public class LogProducer : ProducerBase<LogIntegrationEvent>
{
    private readonly ConnectionFactory _connectionFactory;

    public LogProducer(ConnectionFactory connectionFactory,
        ILogger<RabbitMqClientBase> logger,
        ILogger<ProducerBase<LogIntegrationEvent>> producerBaseLogger) : base(connectionFactory, logger, producerBaseLogger)
    {
        _connectionFactory = connectionFactory;
    }

    protected override string ExchangeName => $"{_connectionFactory.VirtualHost}-moneyRates-direct-exchange";
    protected override string RoutingKeyName => $"{_connectionFactory.VirtualHost}-moneyRates-routing-key";
    protected override string AppId => "LogProducer";
}