using RabbitMqProvider.Client.Producer;
using RabbitMqProvider.Connection;
using RabbitMqProvider.Models;
using Tx.Core.Extensions.String;

namespace ExchangeHttpWorker.DependencyInjection
{
    public static class RabbitMqConfigure
    {
        public static object AddRabbitMqConnection(this IServiceCollection services, HostBuilderContext hostContext)
        {
            var config = hostContext.Configuration;
            var setting = new RabbitMqConfigurationSettings
            {
                RabbitMqConsumerConcurrency = config["RABBITMQ_CONSUMER_CONCURRENCY"].ToInt(50),
                RabbitMqHostname = config["RABBITMQ_HOST_NAME"],
                RabbitMqPort = config["RABBITMQ_PORT"].ToInt(5672),
                RabbitMqPassword = config["RABBITMQ_USER_PASS"],
                RabbitMqUsername = config["RABBITMQ_USER_NAME"],
            };

            var rabbitMqConnection = new RabbitMqConnection(setting);


            services.AddSingleton<IRabbitMqConnection>(new RabbitMqConnection(setting));
            services.AddSingleton<IMessageProducer, RabbitMqProducer>();
            return services;
        }
        
    }
}
