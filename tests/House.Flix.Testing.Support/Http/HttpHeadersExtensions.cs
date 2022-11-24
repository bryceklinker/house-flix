using System.Net.Http.Headers;

namespace House.Flix.Testing.Support.Http;

public static class HttpHeadersExtensions
{
    public static void CopyTo(this HttpHeaders source, HttpHeaders target)
    {
        foreach (var (key, value) in source)
            target.TryAddWithoutValidation(key, value);
    }
}
