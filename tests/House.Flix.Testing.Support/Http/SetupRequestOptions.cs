using System.Net;

namespace House.Flix.Testing.Support.Http;

public record SetupRequestOptions(
    HttpStatusCode StatusCode = HttpStatusCode.OK,
    Func<HttpRequestMessage, Task>? CaptureAsync = null,
    Action<HttpRequestMessage>? Capture = null
)
{
    public static readonly SetupRequestOptions Default = new();

    public static SetupRequestOptions FromCapture(Action<HttpRequestMessage> capture)
    {
        return new SetupRequestOptions(Capture: capture);
    }
}
