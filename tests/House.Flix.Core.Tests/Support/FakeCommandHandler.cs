using House.Flix.Core.Common.Cqrs.Commands;
using MediatR;

namespace House.Flix.Core.Tests.Support;

public class FakeCommand : ICommand
{
    
}

public class FakeCommandHandler : ICommandHandler<FakeCommand>
{
    public Task<Unit> Handle(FakeCommand request, CancellationToken cancellationToken)
    {
        return Task.FromResult(Unit.Value);
    }
}