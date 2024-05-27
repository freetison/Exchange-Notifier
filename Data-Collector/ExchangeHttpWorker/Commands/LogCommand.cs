using MediatR;

namespace ExchangeHttpWorker.Commands;

public class LogCommand : IRequest
{
    public Guid Id { get; set; }
    public string Message { get; set; }
}