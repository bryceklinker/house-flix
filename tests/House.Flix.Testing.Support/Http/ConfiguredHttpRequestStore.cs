using System.Collections.Concurrent;

namespace House.Flix.Testing.Support.Http;

public class ConfiguredHttpRequestStore
{
    private readonly ConcurrentBag<ConfiguredHttpRequest> _requests = new();

    public async Task<HttpResponseMessage?> FindResponseForRequest(HttpRequestMessage request)
    {
        var configuredRequest = _requests.Reverse().FirstOrDefault(c => c.IsMatch(request));

        if (configuredRequest == null)
            return null;

        return await configuredRequest.CreateResponseAsync(request);
    }

    public void Configure(
        HttpRequestMessage request,
        Func<HttpRequestMessage, Task<HttpResponseMessage>> responseFactory,
        SetupRequestOptions? options
    )
    {
        _requests.Add(new ConfiguredHttpRequest(request, responseFactory, options));
    }
}
