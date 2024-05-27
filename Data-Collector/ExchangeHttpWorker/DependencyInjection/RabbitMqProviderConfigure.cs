using ExchangeHttpWorker.RabbitMq.Producer;
using RabbitMQ.Client;
using RabbitMqProvider.Client.Producer;
using RabbitMqProvider.Connection;
using RabbitMqProvider.Models;
using Tx.Core.Extensions.String;

namespace ExchangeHttpWorker.DependencyInjection;

public static class RabbitMqProviderConfigure
{
    public static void AddRabbitMqProvider(this IServiceCollection services, HostBuilderContext hostContext)
    {
        var config = hostContext.Configuration;
        //var setting = new RabbitMqConfigurationSettings
        //{
        //    RabbitMqConsumerConcurrency = config["RABBITMQ_CONSUMER_CONCURRENCY"].ToInt(50),
        //    RabbitMqHostname = config["RABBITMQ_HOST_NAME"],
        //    RabbitMqPort = config["RABBITMQ_PORT"].ToInt(5672),
        //    RabbitMqPassword = config["RABBITMQ_USER_PASS"],
        //    RabbitMqUsername = config["RABBITMQ_USER_NAME"],
        //};

        //services.AddSingleton<RabbitMqConfigurationSettings>(setting);

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
        // services.AddSingleton<IRabbitMqClientProvider>(provider =>
        // {
        //     var factory = new ConnectionFactory
        //     {
        //         HostName = config["RABBITMQ_HOST_NAME"],
        //         Port = config["RABBITMQ_PORT"].ToInt(5672),
        //         UserName = config["RABBITMQ_USER_NAME"],
        //         Password = config["RABBITMQ_USER_PASS"],
        //         DispatchConsumersAsync = true,
        //         AutomaticRecoveryEnabled = true,
        //         ConsumerDispatchConcurrency = config["RABBITMQ_CONSUMER_CONCURRENCY"].ToInt(50),
        //     };
        //
        //     return new RabbitMqClientProvider(factory);
        // });

        
        
        services.AddSingleton<IRabbitMqProducer<LogIntegrationEvent>, ExchangeRatePublisher>();
        services.AddSingleton(serviceProvider =>
       {
           var uri = new Uri($"amqp://{config["RABBITMQ_USER_NAME"]}:{config["RABBITMQ_USER_PASS"]}@{config["RABBITMQ_HOST_NAME"]}:{config["RABBITMQ_PORT"].ToInt(5672)}/{config["RABBITMQ_VIRTUALHOST"]}");
           return new ConnectionFactory
           {
               Uri = uri,
               DispatchConsumersAsync = false,
               ConsumerDispatchConcurrency = config["RABBITMQ_CONSUMER_CONCURRENCY"].ToInt(50),
           };
       });

    }

}