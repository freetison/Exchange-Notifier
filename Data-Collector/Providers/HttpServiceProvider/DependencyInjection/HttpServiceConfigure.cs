// ReSharper disable InconsistentNaming
using Flurl.Http;
using Flurl.Http.Configuration;
using HttpServiceProvider.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace HttpServiceProvider.DependencyInjection;

public static class TelegramBotProviderServicesConfigure {
    
    public static object AddHttpProviders(this IServiceCollection services,HostBuilderContext hostContext)
    {
        var config = hostContext.Configuration;
            
        services.AddSingleton(sp => new FlurlClientCache()
            .Add("TelegramBot", $"{config["TELEGRAM_BOT_BASE_URL"]}{config["TELEGRAM_BOT_TOKEN"]}", builder => builder
                .WithSettings(s => s.Timeout = TimeSpan.FromSeconds(10))
                .BeforeCall(c=> Console.WriteLine($"Calling: {c.Request.Url}"))
            )
            .Add("RapidApi", config["RAPIDAPI_BASE_URL"], builder => builder
                .WithSettings(s => s.Timeout = TimeSpan.FromSeconds(10))
                .WithHeaders(new Dictionary<string, object?>
                {
                    { "X-RapidAPI-Key", config["X-RAPIDAPI-KEY"] },
                    { "X-RapidAPI-Host", config["X-RAPIDAPI-HOST"] }
                })
                .BeforeCall(c=> Console.WriteLine($"Calling: {c.Request.Url}"))
            )
        );


        services.AddSingleton<ITelegramBotClient, TelegramBotClient>();
        services.AddSingleton<IRapidApiClient, RapidApiClient>();
        return services;
            
    }
        
}