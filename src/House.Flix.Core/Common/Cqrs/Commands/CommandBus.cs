using MediatR;

namespace House.Flix.Core.Common.Cqrs.Commands;

public interface ICommandBus
{
    Task<TResult> SendCommandAsync<TResult>(ICommand<TResult> command);
    Task SendCommandAsync(ICommand command);
}

internal class CommandBus : ICommandBus
{
    private readonly IMediator _mediator;

    public CommandBus(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task<TResult> SendCommandAsync<TResult>(ICommand<TResult> command) 
    {
        return await _mediator.Send(command);
    }

    async Task ICommandBus.SendCommandAsync(ICommand command)
    {
        await SendCommandAsync(command);
    }
}