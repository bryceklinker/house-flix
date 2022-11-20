using House.Flix.Core.Common.Cqrs.Queries;

namespace House.Flix.Core.Tests.Support;

public record FakeQuery : IQuery<object>;

public class FakeQueryHandler : IQueryHandler<FakeQuery, object>
{
    public Task<object> Handle(FakeQuery request, CancellationToken cancellationToken)
    {
        return Task.FromResult(new object());
    }
}