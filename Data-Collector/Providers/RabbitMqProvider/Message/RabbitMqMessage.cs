namespace RabbitMqProvider.Message;

public class RabbitMqMessage<TBody> : IRabbitMqMessage
{
    public Guid MessageId { get; set; }
    public TimeSpan TimeToLive { get; set; }
    public TBody Body { get; init; }
}

