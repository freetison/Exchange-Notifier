using ExchangeHttpWorker.EventHandlers;
using HttpServiceProvider.Services;
using MediatR;
using Polly;
using Tx.Core.Extensions.String;

namespace ExchangeHttpWorker.Workers
{

    public class HttpServiceWorker(IConfiguration configuration, IRapidApiClient rapidApiClient, IMediator mediator)
        : BackgroundService
    {
        private readonly IConfiguration _configuration = configuration;


        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var requestRate = _configuration["REQUEST_RATE_IN_SECONDS"].ToDouble(60);
            await Policy
                .HandleResult<bool>(c => c == false)
                .WaitAndRetryForeverAsync(_ => TimeSpan.FromSeconds(requestRate))
                .ExecuteAsync(async () =>
                {
                    await BackgroundProcessing(stoppingToken);
                    return false;
                });

        }

        private async Task BackgroundProcessing(CancellationToken stoppingToken)
        {
            var exchangeRates = await rapidApiClient
                .GetExchangeRates();

            await mediator.Publish(new ExchangeRatesEvent(exchangeRates), stoppingToken);
        }

    }
       
}
