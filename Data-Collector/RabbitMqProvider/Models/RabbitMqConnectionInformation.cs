namespace RabbitMqProvider.Models;

public class RabbitMqConnectionInformation
{
    public string? QueueName { get; set; }
    public string? Exchange { get; set; }
    public string? RoutingKey { get; set; }
}