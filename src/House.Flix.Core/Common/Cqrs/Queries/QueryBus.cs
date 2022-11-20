using MediatR;

namespace House.Flix.Core.Common.Cqrs.Queries;

public interface IQueryBus
{
    Task<TResult> SendQueryAsync<TResult>(IQuery<TResult> query);
}

internal class QueryBus : IQueryBus
{
    private readonly IMediator _mediator;

    public QueryBus(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task<TResult> SendQueryAsync<TResult>(IQuery<TResult> query)
    {
        return await _mediator.Send(query).ConfigureAwait(false);
    }
}