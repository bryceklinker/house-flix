using House.Flix.Core.Common.Logging;
using MediatR;
using Microsoft.Extensions.Logging;

namespace House.Flix.Core.Common.Cqrs.Events;

public interface IEventBus
{
    Task PublishEventAsync(IEvent @event);
}

internal class EventBus : IEventBus
{
    private readonly IMediator _mediator;
    private readonly ILogger<EventBus> _logger;

    public EventBus(IMediator mediator, ILogger<EventBus> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    public async Task PublishEventAsync(IEvent @event)
    {
        await _logger.Capture(@event, () => _mediator.Publish(@event)).ConfigureAwait(false);
    }
}
