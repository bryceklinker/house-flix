using MediatR;

namespace House.Flix.Core.Common.Cqrs.Commands;

public interface ICommand<out TResult> : IRequest<TResult>
{
}

public interface ICommand : ICommand<Unit>
{
}

public interface ICommandHandler<in TCommand, TResult> : IRequestHandler<TCommand, TResult>
    where TCommand : ICommand<TResult>
{
}

public interface ICommandHandler<in TCommand> : ICommandHandler<TCommand, Unit>
    where TCommand : ICommand
{
}