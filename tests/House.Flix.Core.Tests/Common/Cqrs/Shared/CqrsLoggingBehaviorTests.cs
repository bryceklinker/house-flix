using House.Flix.Core.Common.Cqrs;
using House.Flix.Core.Tests.Support;
using House.Flix.Testing.Support.Logging;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace House.Flix.Core.Tests.Common.Cqrs.Shared;

public class CqrsLoggingBehaviorTests
{
    private readonly FakeLogger _logger;
    private readonly ICqrsBus _bus;

    public CqrsLoggingBehaviorTests()
    {
        var provider = ServiceProviderFactory.Create();
        _logger = provider.GetRequiredService<FakeLogger>();
        _bus = provider.GetRequiredService<ICqrsBus>();
    }

    [Fact]
    public async Task WhenCommandIsSentThenCommandIsLogged()
    {
        await _bus.SendCommandAsync(new FakeCommand());
        
        _logger.GetEntries(LogLevel.Information).Should().HaveCount(2);
    }

    [Fact]
    public async Task WhenQueryIsSentThenQueryIsLogged()
    {
        await _bus.SendQueryAsync(new FakeQuery());

        _logger.GetEntries(LogLevel.Information).Should().HaveCount(2);
    }
    
    [Fact]
    public async Task WhenEventIsSentThenEventIsLogged()
    {
        await _bus.SendEventAsync(new FakeEvent());

        _logger.GetEntries(LogLevel.Information).Should().HaveCount(2);
    }
}