using House.Flix.Core.Common.Cqrs.Commands;
using House.Flix.Core.Common.Cqrs.Events;
using House.Flix.Core.Common.Cqrs.Queries;

namespace House.Flix.Core.Common.Cqrs;

public interface ICqrsBus : IEventBus, ICommandBus, IQueryBus { }

public class CqrsBus : ICqrsBus
{
    private readonly ICommandBus _commandBus;
    private readonly IQueryBus _queryBus;
    private readonly IEventBus _eventBus;

    public CqrsBus(ICommandBus commandBus, IQueryBus queryBus, IEventBus eventBus)
    {
        _commandBus = commandBus;
        _queryBus = queryBus;
        _eventBus = eventBus;
    }

    public async Task PublishEventAsync(IEvent @event)
    {
        await _eventBus.PublishEventAsync(@event).ConfigureAwait(false);
    }

    public async Task<TResult> SendCommandAsync<TResult>(ICommand<TResult> command)
    {
        return await _commandBus.SendCommandAsync(command).ConfigureAwait(false);
    }

    public async Task SendCommandAsync(ICommand command)
    {
        await _commandBus.SendCommandAsync(command).ConfigureAwait(false);
    }

    public async Task<TResult> SendQueryAsync<TResult>(IQuery<TResult> query)
    {
        return await _queryBus.SendQueryAsync(query).ConfigureAwait(false);
    }
}
