using House.Flix.Core.Common.Logging;
using MediatR;
using Microsoft.Extensions.Logging;

namespace House.Flix.Core.Common.Cqrs.Shared;

public class CqrsLoggingMiddleware<TInput, TOutput> : IPipelineBehavior<TInput, TOutput>
    where TInput : IRequest<TOutput>
{
    private readonly ILogger<CqrsLoggingMiddleware<TInput, TOutput>> _logger;

    public CqrsLoggingMiddleware(ILogger<CqrsLoggingMiddleware<TInput, TOutput>> logger)
    {
        _logger = logger;
    }

    public async Task<TOutput> Handle(TInput request, RequestHandlerDelegate<TOutput> next, CancellationToken cancellationToken)
    {
        return await _logger.Capture(request, () => next()).ConfigureAwait(false);
    }
}