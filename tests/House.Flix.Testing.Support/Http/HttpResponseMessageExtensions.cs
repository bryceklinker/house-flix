namespace House.Flix.Testing.Support.Http;

public static class HttpResponseMessageExtensions
{
    public static async Task<HttpResponseMessage> CloneAsync(
        this HttpResponseMessage original,
        HttpRequestMessage? request = null
    )
    {
        var copy = new HttpResponseMessage(original.StatusCode)
        {
            Content = await original.Content.CloneAsync()
        };
        original.Headers.CopyTo(copy.Headers);
        return copy;
    }
}
