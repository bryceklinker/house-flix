using House.Flix.Core.Common.Cqrs.Events;

namespace House.Flix.Core.Common.Events;

public record VideoFileLocatedEvent(string FilePath) : IEvent;
