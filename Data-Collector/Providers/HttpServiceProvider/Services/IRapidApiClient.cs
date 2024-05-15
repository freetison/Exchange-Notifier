using HttpServiceProvider.Models.RapidApi;

namespace HttpServiceProvider.Services;

public interface IRapidApiClient
{
    Task<ExchangeRates> GetExchangeRates();
    Task<ConvertRate> GetConvertRate(string from, string to, decimal amount);

}