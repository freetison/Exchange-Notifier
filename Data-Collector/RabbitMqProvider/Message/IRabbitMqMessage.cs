namespace RabbitMqProvider.Message;

public interface IRabbitMqMessage
{
    Guid MessageId { get; set; }
    TimeSpan TimeToLive { get; set; }
}