namespace House.Flix.Testing.Support.Http;

public static class HttpContentExtensions
{
    public static async Task<HttpContent?> CloneAsync(this HttpContent? original)
    {
        if (original == null)
            return null;

        var stream = new MemoryStream();
        await original.CopyToAsync(stream);
        stream.Position = 0;
        var copy = new StreamContent(stream);
        original.Headers.CopyTo(copy.Headers);
        return copy;
    }
}
