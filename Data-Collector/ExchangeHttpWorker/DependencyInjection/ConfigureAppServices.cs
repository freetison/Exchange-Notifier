﻿using ExchangeHttpWorker.Workers;
using HttpServiceProvider.DependencyInjection;

namespace ExchangeHttpWorker.DependencyInjection
{
    public static class ConfigureAppServices
    {
        public static void ConfigureServices(HostBuilderContext hostContext, IServiceCollection services)
        {
            services.AddLogging();
            services.AddAddMediatRService();
            services.AddRabbitMqConnection(hostContext);
            services.AddRabbitMqProvider(hostContext);
            services.AddHttpProviders(hostContext);
            services.AddHostedService<HttpServiceWorker>();
            // services.AddHostedService<NotifierServiceWorker>();

        }

    }
}
