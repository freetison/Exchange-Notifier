using ExchangeHttpWorker.Commands;
using MediatR;

namespace ExchangeHttpWorker.EventHandlers;

public class LogCommandHandler(ILogger<LogCommandHandler> logger) : IRequestHandler<LogCommand>
{
    public Task Handle(LogCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("---- Received message: {Message} ----", request.Message);
        return Task.CompletedTask;
    }
}