namespace House.Flix.Testing.Support.Http;

public record ConfiguredHttpRequest(
    HttpRequestMessage Request,
    Func<HttpRequestMessage, Task<HttpResponseMessage>> ResponseFactory,
    SetupRequestOptions? Options = null
)
{
    public SetupRequestOptions Options { get; } = Options ?? SetupRequestOptions.Default;
    private Func<HttpRequestMessage, Task> Capture => Options.Capture ?? (_ => Task.CompletedTask);

    public bool IsMatch(HttpRequestMessage request)
    {
        return request.Method == Request.Method && request.RequestUri == Request.RequestUri;
    }

    public async Task<HttpResponseMessage> CreateResponseAsync(HttpRequestMessage request)
    {
        await Capture.Invoke(request);
        var response = await ResponseFactory.Invoke(request);
        response.StatusCode = Options.StatusCode;
        return await response.CloneAsync(request).ConfigureAwait(false);
    }
}
