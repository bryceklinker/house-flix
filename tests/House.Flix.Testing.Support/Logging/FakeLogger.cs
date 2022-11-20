using System.Collections.Concurrent;
using Microsoft.Extensions.Logging;

namespace House.Flix.Testing.Support.Logging;

public record FakeLogEntry(LogLevel Level, EventId EventId, object? State, Exception? Exception, string Message);

public class FakeLogger : ILogger, IDisposable
{
    private readonly ConcurrentBag<FakeLogEntry> _entries = new();

    public FakeLogEntry[] GetEntries(LogLevel level)
    {
        return _entries.Where(e => e.Level == level).ToArray();
    }
    
    public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter)
    {
        var message = formatter(state, exception);
        _entries.Add(new FakeLogEntry(logLevel, eventId, state, exception, message));
    }

    public bool IsEnabled(LogLevel logLevel)
    {
        return true;
    }

    public IDisposable BeginScope<TState>(TState state)
        where TState : notnull
    {
        return this;
    }

    public void Dispose()
    {
    }
}

public class FakeLoggerFactory : ILoggerFactory
{
    private readonly FakeLogger _logger = new FakeLogger();

    public FakeLogger Logger => _logger;
    
    public void Dispose()
    {
        
    }

    public ILogger CreateLogger(string categoryName)
    {
        return _logger;
    }

    public void AddProvider(ILoggerProvider provider)
    {
        
    }
}