using RabbitMQ.Client;
using RabbitMqProvider.Client.Producer;
using RabbitMqProvider.Models;

namespace ExchangeHttpWorker.RabbitMq.Producer;

public class ExchangeRatePublisher(
    ConnectionFactory connectionFactory,
    ILogger<ProducerBase<LogIntegrationEvent>> producerBaseLogger)
    : ProducerBase<LogIntegrationEvent>(connectionFactory, producerBaseLogger)
{
    private readonly ConnectionFactory _connectionFactory = connectionFactory;

    protected override string ExchangeName => $"moneyRates-direct-exchange";
    protected override string RoutingKeyName => $"moneyRates-routing-key";
    protected override string AppId => "ExchangeRatePublisher";
}