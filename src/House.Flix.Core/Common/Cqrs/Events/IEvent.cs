using MediatR;

namespace House.Flix.Core.Common.Cqrs.Events;

public interface IEvent : INotification
{
    
}

public interface IEventHandler<TEvent> : INotificationHandler<TEvent>
    where TEvent : IEvent
{
    
}