using System.Net;

namespace House.Flix.Testing.Support.Http;

public record SetupRequestOptions(
    HttpStatusCode StatusCode = HttpStatusCode.OK,
    Func<HttpRequestMessage, Task>? Capture = null
)
{
    public static readonly SetupRequestOptions Default = new();
}
