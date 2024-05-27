using HttpServiceProvider.Services;
using MediatR;

namespace ExchangeHttpWorker.EventHandlers;

public class TelegramBotProcessorHandler(ILogger<TelegramBotProcessorHandler> logger, ITelegramBotClient telegramBotClient)
    : INotificationHandler<ExchangeRatesEvent>
{
    private readonly ITelegramBotClient _telegramBotClient = telegramBotClient;

    public async Task Handle(ExchangeRatesEvent notification, CancellationToken cancellationToken)
    {
        // esto debe ocurrir cuando lea de la queue, el resultado del rule-processor

        //var message = notification.Data.ToJson();
        //var result = await _telegramBotClient.SendMessageAsync(-4277423125, message);

        // logger.LogInformation($"Message was sent to Telegram");
    }
   
}