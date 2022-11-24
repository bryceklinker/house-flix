using System.Collections.Concurrent;

namespace House.Flix.Testing.Support.Http;

public class FakeHttpClientFactory : IHttpClientFactory
{
    private readonly ConcurrentDictionary<string, FakeHttpMessageHandler> _handlers = new();

    public FakeHttpMessageHandler GetHandler(string? name = null)
    {
        return _handlers.GetOrAdd(name ?? "default", _ => new FakeHttpMessageHandler());
    }

    public HttpClient CreateClient(string name)
    {
        return new HttpClient(GetHandler(name));
    }
}
