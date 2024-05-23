using ExchangeHttpWorker.EventHandlers;
using HttpServiceProvider.Services;
using MediatR;
using Polly;

using RabbitMqProvider.Connection;

using Tx.Core.Extensions.String;

namespace ExchangeHttpWorker.Workers
{
    public class NotifierServiceWorker(IConfiguration configuration, IRabbitMqClientProvider rabbitMqClientProvider, IMediator mediator)
        : BackgroundService
    {
        private readonly IConfiguration _configuration = configuration;


        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            // Ejemplo de nombre de cola
            string queueName = "testQueue";

            await Policy
                .HandleResult<bool>(c => c == false)
                .RetryForeverAsync()
                .ExecuteAsync(async () =>
                {
                    await BackgroundProcessing(queueName, stoppingToken);
                    return false;
                });
           
        }

        private async Task BackgroundProcessing(string queueName, CancellationToken stoppingToken)
        {
             rabbitMqClientProvider.ReadMessages(queueName, ProcessMessage);
            
            //await mediator.Publish(new ExchangeRatesEvent(exchangeRates), stoppingToken);

            
        }

        private void ProcessMessage(string message)
        {
            // Implementar la lógica para procesar el mensaje recibido
            Console.WriteLine($"Mensaje recibido: {message}");
        }


    }

  
}
