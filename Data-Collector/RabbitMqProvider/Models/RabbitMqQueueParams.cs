namespace RabbitMqProvider.Models;

public class RabbitMqQueueParams
{
    public bool Durable { get; set; } = false;
    public bool Exclusive { get; set; } = false;
    public bool AutoDelete { get; set; } = false;
    public IDictionary<string, object>? Arguments  { get; set; } = null;
}