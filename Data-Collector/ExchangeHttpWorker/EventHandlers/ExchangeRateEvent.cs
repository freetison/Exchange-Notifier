using HttpServiceProvider.Models.RapidApi;
using MediatR;

namespace ExchangeHttpWorker.EventHandlers
{
    public class ExchangeRatesEvent(ExchangeRates data) : INotification
    {
        public ExchangeRates Data { get; set; } = data;
    }
}