using Flurl.Http;
using Flurl.Http.Configuration;
using HttpServiceProvider.Models.RapidApi;

namespace HttpServiceProvider.Services
{
    public class RapidApiClient(IFlurlClientCache clients) : IRapidApiClient
    {
        private readonly IFlurlClient _flurlCli = clients.Get("RapidApi");

        public async Task<ExchangeRates> GetExchangeRates()
        {
            return await _flurlCli
                .Request("latest")
                .GetJsonAsync<ExchangeRates>();
        }

        public async Task<ConvertRate> GetConvertRate(string from, string to, decimal amount)
        {
            return await _flurlCli
                .Request("convert")
                .SetQueryParams(new { from = from, to = to, amount = amount })
                .GetJsonAsync<ConvertRate>();
        }
    }

}