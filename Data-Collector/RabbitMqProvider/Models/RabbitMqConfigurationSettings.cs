namespace RabbitMqProvider.Models;

public class RabbitMqConfigurationSettings
{
    public string? RabbitMqHostname { get; set; } = "localhost";
    public string? RabbitMqUsername { get; set; }
    public string? RabbitMqPassword { get; set; }
    public int RabbitMqPort { get; set; } = 5672;
    public int? RabbitMqConsumerConcurrency { get; set; }
}
