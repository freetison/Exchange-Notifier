using ExchangeHttpWorker.Services.Http;
using Flurl.Http;
using Flurl.Http.Configuration;

namespace ExchangeHttpWorker.DependencyInjection
{
    public static class HttpServicesConfigure
    {
        public static object AddHttpServices(this IServiceCollection services, HostBuilderContext hostContext)
        {
            var config = hostContext.Configuration;
            services.AddSingleton(sp => new FlurlClientCache()
                .Add("RapidApi", config["RAPIDAPI_BASE_URL"], builder => builder
                    .WithSettings(s => s.Timeout = TimeSpan.FromSeconds(10))
                    .WithHeaders(new Dictionary<string, object>
                    {
                        { "X-RapidAPI-Key", config["X-RAPIDAPI-KEY"] },
                        { "X-RapidAPI-Host", config["X-RAPIDAPI-HOST"] }
                    })
                    .BeforeCall(c=> Console.WriteLine($"Calling: {c.Request.Url}"))
                ));

            services.AddSingleton<IRapidApiClient, RapidApiClient>();
            
            return services;
        }
        
    }
}
