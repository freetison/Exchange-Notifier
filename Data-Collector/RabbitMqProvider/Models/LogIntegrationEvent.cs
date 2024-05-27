namespace RabbitMqProvider.Models;

public class LogIntegrationEvent
{
    public Guid Id { get; set; }
    public string? Message { get; set; }
}