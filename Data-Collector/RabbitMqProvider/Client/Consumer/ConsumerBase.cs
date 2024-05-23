using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RabbitMQ.Client.Events;
using RabbitMQ.Client;
using System.Text;

namespace RabbitMqProvider.Client.Consumer
{
    public abstract class ConsumerBase(
        ConnectionFactory connectionFactory,
        ILogger<ConsumerBase> consumerLogger,
        ILogger<RabbitMqClientBase> logger)
        : RabbitMqClientBase(connectionFactory, logger)
    {
        protected virtual string QueueName { get; set; } = connectionFactory.VirtualHost;

        protected virtual Task<T> OnEventReceived<T>(object sender, BasicDeliverEventArgs @event)
        {
            T? message = default(T);
            
            try
            {
                var body = Encoding.UTF8.GetString(@event.Body.ToArray());
                message = JsonConvert.DeserializeObject<T>(body);
            }
            catch (Exception ex)
            {
                consumerLogger.LogCritical(ex, "Error while retrieving message from queue.");
            }
            finally
            {
                Channel?.BasicAck(@event.DeliveryTag, false);
            }
            
            return Task.FromResult(message)!;
        }
    }
}
