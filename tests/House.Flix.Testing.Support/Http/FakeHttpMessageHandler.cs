using System.Net;
using System.Net.Http.Json;

namespace House.Flix.Testing.Support.Http;

public class FakeHttpMessageHandler : HttpMessageHandler
{
    private readonly ConfiguredHttpRequestStore _store = new();

    protected override async Task<HttpResponseMessage> SendAsync(
        HttpRequestMessage request,
        CancellationToken cancellationToken
    )
    {
        var response = await _store.FindResponseForRequest(request);
        return response ?? new HttpResponseMessage(HttpStatusCode.NotFound);
    }

    public void SetupHttpRequest(
        HttpRequestMessage request,
        Func<HttpRequestMessage, Task<HttpResponseMessage>> responseFactory,
        SetupRequestOptions? options = null
    )
    {
        _store.Configure(request, responseFactory, options);
    }

    public void SetupHttpRequest(
        HttpRequestMessage request,
        HttpResponseMessage response,
        SetupRequestOptions? options = null
    )
    {
        SetupHttpRequest(request, _ => Task.FromResult(response), options);
    }

    public void SetupJsonResponse<T>(
        HttpRequestMessage request,
        T data,
        SetupRequestOptions? options = null
    )
    {
        var response = new HttpResponseMessage(HttpStatusCode.OK)
        {
            Content = JsonContent.Create(data)
        };
        SetupHttpRequest(request, response, options);
    }

    public void SetupJsonGet<T>(string url, T data, SetupRequestOptions? options = null)
    {
        SetupJsonResponse(new HttpRequestMessage(HttpMethod.Get, url), data, options);
    }
}
