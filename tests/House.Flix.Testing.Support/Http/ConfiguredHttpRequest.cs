namespace House.Flix.Testing.Support.Http;

public record ConfiguredHttpRequest(
    HttpRequestMessage Request,
    Func<HttpRequestMessage, Task<HttpResponseMessage>> ResponseFactory,
    SetupRequestOptions? Options = null
)
{
    public SetupRequestOptions Options { get; } = Options ?? SetupRequestOptions.Default;
    private Func<HttpRequestMessage, Task> CaptureAsync =>
        Options.CaptureAsync ?? (_ => Task.CompletedTask);
    private Action<HttpRequestMessage> Capture => Options.Capture ?? (_ => { });

    public bool IsMatch(HttpRequestMessage request)
    {
        return request.Method == Request.Method
            && request.RequestUri!.IsBaseOf(request.RequestUri!);
    }

    public async Task<HttpResponseMessage> CreateResponseAsync(HttpRequestMessage request)
    {
        await CaptureAsync.Invoke(request);
        Capture.Invoke(request);
        var response = await ResponseFactory.Invoke(request);
        response.StatusCode = Options.StatusCode;
        return await response.CloneAsync(request).ConfigureAwait(false);
    }
}
