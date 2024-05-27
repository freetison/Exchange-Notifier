using MediatR;
using RabbitMqProvider.Client.Producer;
using RabbitMqProvider.Models;
using Tx.Core.Extensions.String;

namespace ExchangeHttpWorker.EventHandlers;

public class ExchangeRatePublisherHandler(
    ILogger<ExchangeRatePublisherHandler> logger,
    IServiceScopeFactory scopeFactory,
    IRabbitMqProducer<LogIntegrationEvent> producer)
    : INotificationHandler<ExchangeRatesEvent>
{
    private readonly ILogger _logger = logger;


    public Task Handle(ExchangeRatesEvent notification, CancellationToken cancellationToken)
    {
        // using var scope = scopeFactory.CreateScope();
        // var rabbitMqClientProvider = scope.ServiceProvider.GetRequiredService<IMessageProducer>();
        // var message = new RabbitMqMessage<string>()
        // {
        //     TimeToLive = TimeSpan.FromMinutes(1),
        //     Body = notification.Data.ToJson()
        // };

        // await rabbitMqClientProvider.SendMessageAsync(message, "", "ExchangeMonitor");
        
        
        var @event = new LogIntegrationEvent
        {
            Id = Guid.NewGuid(),
            Message = notification.Data.ToJson()
        };

        // producer.Publish(@event);
        //
        // _logger.LogInformation($"Message was sent to the queue");
        return Task.CompletedTask;
    }

}

