using RabbitMqProvider.Connection;
using RabbitMqProvider.Models;
using RabbitMqProvider.Producer;
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
                RabbitMqPort = config["RABBITMQ_PORT"].ToInt(15672),
                RabbitMqPassword = config["RABBITMQ_USER_PASS"],
                RabbitMqUsername = config["RABBITMQ_USER_NAME"],
            };

            services.AddSingleton<IRabbitMqConnection>(new RabbitMqConnection(setting));
            services.AddSingleton<IMessageProducer, RabbitMqProducer>();
            return services;
        }
        
    }
}
