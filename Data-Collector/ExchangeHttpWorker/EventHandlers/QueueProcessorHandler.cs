using MediatR;
using RabbitMqProvider.Client.Producer;
using RabbitMqProvider.Message;
using RabbitMqProvider.Models;
using Tx.Core.Extensions.String;

namespace ExchangeHttpWorker.EventHandlers;

public class QueueProcessorHandler(
    ILogger<QueueProcessorHandler> logger,
    IServiceScopeFactory scopeFactory,
    IRabbitMqProducer<LogIntegrationEvent> producer)
    : INotificationHandler<ExchangeRatesEvent>
{
    private readonly ILogger _logger = logger;


    public async Task Handle(ExchangeRatesEvent notification, CancellationToken cancellationToken)
    {
        using var scope = scopeFactory.CreateScope();
        var rabbitMqClientProvider = scope.ServiceProvider.GetRequiredService<IMessageProducer>();
        var message = new RabbitMqMessage<string>()
        {
            TimeToLive = TimeSpan.FromMinutes(1),
            Body = notification.Data.ToJson()
        };

        // await rabbitMqClientProvider.SendMessageAsync(message, "", "ExchangeMonitor");
        
        
        var @event = new LogIntegrationEvent
        {
            Id = Guid.NewGuid(),
            Message = $"Hello! Message generated at {DateTime.Now.ToString("O")}"
        };

        producer.Publish(@event);

        _logger.LogInformation($"Message was sent to the queue");
    }

}

