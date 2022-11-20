using House.Flix.Core.Common.Cqrs.Events;

namespace House.Flix.Core.Tests.Support;

public record FakeEvent : IEvent;

public class FakeEventHandler : IEventHandler<FakeEvent>
{
    public Task Handle(FakeEvent notification, CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}