using MediatR;

namespace ExchangeHttpWorker.EventHandlers;

public class LogEvent : INotification
{
    public Guid Id { get; set; }
    public string? Message { get; set; }
}