using MediatR;
using RabbitMqProvider.Connection;
using Tx.Core.Extensions.String;

namespace ExchangeHttpWorker.EventHandlers;

public class ExchangeRateRabbitMqPublisherHandler : INotificationHandler<ExchangeRatesEvent>
{
    private readonly ILogger<ExchangeRateRabbitMqPublisherHandler> _logger;
    private readonly IServiceScopeFactory _scopeFactory;

    public ExchangeRateRabbitMqPublisherHandler(ILogger<ExchangeRateRabbitMqPublisherHandler> logger, IServiceScopeFactory scopeFactory)
    {
        _logger = logger;
        _scopeFactory = scopeFactory;
    }

    public Task Handle(ExchangeRatesEvent notification, CancellationToken cancellationToken)
    {
        
        using var scope = _scopeFactory.CreateScope();
        var rabbitMqClientProvider = scope.ServiceProvider.GetRequiredService<IRabbitMqClientProvider>();
        rabbitMqClientProvider.PublishMessage<string>(notification.Data.ToJson());
        _logger.LogInformation($"Message was sent to the queue");
        return Task.CompletedTask;
    }

}

