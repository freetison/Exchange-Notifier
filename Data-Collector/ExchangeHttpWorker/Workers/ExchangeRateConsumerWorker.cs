using System.Text;
using MediatR;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMqProvider.Client.Consumer;

namespace ExchangeHttpWorker.Workers
{
    public sealed class ExchangeRateConsumerWorker : ConsumerBase, IHostedService
    {
        protected override string QueueName { get; set; } = "moneyRates-queue";
        private readonly ILogger<ConsumerBase> _baseLogger;
        private readonly ILogger<ExchangeRateConsumerWorker> _logger;

        public ExchangeRateConsumerWorker(
            IMediator mediator,
            ConnectionFactory connectionFactory,
            ILogger<ExchangeRateConsumerWorker> logConsumerLogger,
            ILogger<ConsumerBase> baseLogger)
            : base(connectionFactory, baseLogger)
        {
            _logger = logConsumerLogger;
            _baseLogger = baseLogger;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("RabbitMQ consumer started.");
            
            try
            {
                var consumer = new AsyncEventingBasicConsumer(Channel);
                // consumer.Received += OnEventReceived<LogCommand>;
             
                consumer.Received += (model, ea) =>
                {
                    var body = ea.Body.ToArray();
                    var message = Encoding.UTF8.GetString(body);
                    _logger.LogInformation($" [x] Received {message}");
                    Channel?.BasicAck(ea.DeliveryTag, false); 
                    return  Task.FromResult(message);
                };

                Channel.BasicConsume(queue: QueueName, autoAck: false, consumer: consumer);
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex, "Error while consuming message");
            }
            
            return  Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            Dispose();
            return Task.CompletedTask;
        }
    }
}
