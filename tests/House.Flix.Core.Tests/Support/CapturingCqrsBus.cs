using System.Collections.Concurrent;
using House.Flix.Core.Common.Cqrs;
using House.Flix.Core.Common.Cqrs.Commands;
using House.Flix.Core.Common.Cqrs.Events;
using House.Flix.Core.Common.Cqrs.Queries;

namespace House.Flix.Core.Tests.Support;

public class CapturingCqrsBus : ICqrsBus
{
    private readonly ConcurrentBag<IEvent> _capturedEvents = new();
    private readonly ConcurrentBag<object> _capturedCommands = new();
    private readonly ConcurrentBag<object> _capturedQueries = new();
    private readonly ICqrsBus _inner;

    public CapturingCqrsBus(ICqrsBus inner)
    {
        _inner = inner;
    }

    public T[] GetEvents<T>() where T : IEvent
    {
        return _capturedEvents.OfType<T>().ToArray();
    }

    public TQuery[] GetQueries<TQuery, TResult>() where TQuery : IQuery<TResult>
    {
        return _capturedQueries.OfType<TQuery>().ToArray();
    }

    public TCommand[] GetCommands<TCommand>() where TCommand : ICommand
    {
        return _capturedCommands.OfType<TCommand>().ToArray();
    }

    public TCommand[] GetCommands<TCommand, TResult>() where TCommand : ICommand<TResult>
    {
        return _capturedCommands.OfType<TCommand>().ToArray();
    }

    public async Task SendEventAsync(IEvent @event)
    {
        _capturedEvents.Add(@event);
        await _inner.SendEventAsync(@event).ConfigureAwait(false);
    }

    public async Task<TResult> SendCommandAsync<TResult>(ICommand<TResult> command)
    {
        _capturedCommands.Add(command);
        return await _inner.SendCommandAsync(command);
    }

    public async Task SendCommandAsync(ICommand command)
    {
        _capturedCommands.Add(command);
        await _inner.SendCommandAsync(command);
    }

    public async Task<TResult> SendQueryAsync<TResult>(IQuery<TResult> query)
    {
        _capturedQueries.Add(query);
        return await _inner.SendQueryAsync(query);
    }
}
