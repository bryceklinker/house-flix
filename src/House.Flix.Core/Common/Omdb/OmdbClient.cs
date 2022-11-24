using System.Net.Http.Json;
using Microsoft.Extensions.Options;

namespace House.Flix.Core.Common.Omdb;

public interface IOmdbClient
{
    Task<OmdbSearchResponseModel?> SearchAsync(OmdbSearchParameters parameters);
    Task<OmdbMovieResponseModel?> GetByTitle(OmdbGetByTitleParameters parameters);
    Task<OmdbMovieResponseModel?> GetById(OmdbGetByIdParameters parameters);
}

public class OmdbClient : IOmdbClient
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly IOptions<OmdbOptions> _options;

    private OmdbOptions Options => _options.Value;

    public OmdbClient(IHttpClientFactory httpClientFactory, IOptions<OmdbOptions> options)
    {
        _httpClientFactory = httpClientFactory;
        _options = options;
    }

    public async Task<OmdbSearchResponseModel?> SearchAsync(OmdbSearchParameters parameters)
    {
        var query = $"?apiKey={Options.ApiKey}&s={parameters.Term}";
        return await GetClient()
            .GetFromJsonAsync<OmdbSearchResponseModel>(query)
            .ConfigureAwait(false);
    }

    public async Task<OmdbMovieResponseModel?> GetByTitle(OmdbGetByTitleParameters parameters)
    {
        var query = $"?apiKey={Options.ApiKey}&t={parameters.Title}";
        return await GetClient()
            .GetFromJsonAsync<OmdbMovieResponseModel>(query)
            .ConfigureAwait(false);
    }

    public async Task<OmdbMovieResponseModel?> GetById(OmdbGetByIdParameters parameters)
    {
        var query = $"?apiKey={Options.ApiKey}&i={parameters.ImdbId}";
        return await GetClient()
            .GetFromJsonAsync<OmdbMovieResponseModel>(query)
            .ConfigureAwait(false);
    }

    private HttpClient GetClient()
    {
        var client = _httpClientFactory.CreateClient("omdb");
        client.BaseAddress = new Uri(Options.BaseUrl);
        return client;
    }
}
