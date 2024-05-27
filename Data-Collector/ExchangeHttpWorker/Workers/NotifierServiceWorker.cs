using ExchangeHttpWorker.Commands;
using MediatR;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMqProvider.Client.Consumer;
using ConnectionFactory = RabbitMQ.Client.ConnectionFactory;

namespace ExchangeHttpWorker.Workers
{
    public sealed class NotifierServiceWorker : ConsumerBase, IHostedService
    {
        protected override string QueueName { get; set; } = "moneyRates-queue";
        
        public NotifierServiceWorker(
            IMediator mediator,
            ConnectionFactory connectionFactory,
            ILogger<NotifierServiceWorker> logConsumerLogger,
            ILogger<ConsumerBase> consumerLogger) :
            base(connectionFactory, consumerLogger)
        {
            try
            {
                var consumer = new AsyncEventingBasicConsumer(Channel);
                consumer.Received += OnEventReceived<LogCommand>;
                Channel.BasicConsume(queue: QueueName, autoAck: false, consumer: consumer);
            }
            catch (Exception ex)
            {
                logConsumerLogger.LogCritical(ex, "Error while consuming message");
            }
        }

        public Task StartAsync(CancellationToken cancellationToken) => Task.CompletedTask;

        public Task StopAsync(CancellationToken cancellationToken)
        {
            Dispose();
            return Task.CompletedTask;
        }

        // protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        // {
        //     // Ejemplo de nombre de cola
        //     string queueName = "testQueue";
        //
        //     await Policy
        //         .HandleResult<bool>(c => c == false)
        //         .RetryForeverAsync()
        //         .ExecuteAsync(async () =>
        //         {
        //             await BackgroundProcessing(queueName, stoppingToken);
        //             return false;
        //         });
        //    
        // }
        //
        // private async Task BackgroundProcessing(string queueName, CancellationToken stoppingToken)
        // {
        //      _rabbitMqClientProvider.ReadMessages(queueName, ProcessMessage);
        //     
        //     //await mediator.Publish(new ExchangeRatesEvent(exchangeRates), stoppingToken);
        //
        //     
        // }
        //
        // private void ProcessMessage(string message)
        // {
        //     // Implementar la lógica para procesar el mensaje recibido
        //     Console.WriteLine($"Mensaje recibido: {message}");
        // }


    }


}
