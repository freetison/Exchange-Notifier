using RabbitMQ.Client;
using RabbitMqProvider.Connection;
using RabbitMqProvider.Models;
using Tx.Core.Extensions.String;

namespace ExchangeHttpWorker.DependencyInjection;

public static class RabbitMqProviderConfigure
{
    public static void AddRabbitMqProvider(this IServiceCollection services, HostBuilderContext hostContext)
    {
        var config = hostContext.Configuration;

        services.AddSingleton<RabbitMqConnectionInformation>(provider => new RabbitMqConnectionInformation
        {
            Exchange = config["RABBITMQ_EXCHANGE"],
            QueueName = config["RABBITMQ_QUEUE_NAME"],
            RoutingKey = config["RABBITMQ_ROUTING_KEY"],
        });

        
        services.AddSingleton<IConnectionFactory>(provider => new ConnectionFactory
        {
            HostName = config["RABBITMQ_HOST_NAME"],
            Port = config["RABBITMQ_PORT"].ToInt(5672),
            UserName = config["RABBITMQ_USER_NAME"],
            Password = config["RABBITMQ_USER_PASS"],
            VirtualHost = config["RABBITMQ_VIRTUALHOST"],
            DispatchConsumersAsync = true,
            AutomaticRecoveryEnabled = true,
            ConsumerDispatchConcurrency = config["RABBITMQ_CONSUMER_CONCURRENCY"].ToInt(50),
        });
        
        services.AddScoped<IRabbitMqClientProvider, RabbitMqClientProvider>();
        
    }

}