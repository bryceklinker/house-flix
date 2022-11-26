using System.Net.Http.Json;
using House.Flix.Core.Common.Http;
using Microsoft.Extensions.Options;

namespace House.Flix.Core.Common.Omdb;

public interface IOmdbClient
{
    Task<OmdbSearchResponseModel?> SearchAsync(OmdbSearchParameters parameters);
    Task<OmdbVideoResponseModel?> GetByTitle(OmdbGetByTitleParameters parameters);
    Task<OmdbVideoResponseModel?> GetById(OmdbGetByIdParameters parameters);
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
        return await GetClient()
            .GetFromJsonAsync<OmdbSearchResponseModel>(BuildQueryString(parameters))
            .ConfigureAwait(false);
    }

    public async Task<OmdbVideoResponseModel?> GetByTitle(OmdbGetByTitleParameters parameters)
    {
        return await GetClient()
            .GetFromJsonAsync<OmdbVideoResponseModel>(BuildQueryString(parameters))
            .ConfigureAwait(false);
    }

    public async Task<OmdbVideoResponseModel?> GetById(OmdbGetByIdParameters parameters)
    {
        return await GetClient()
            .GetFromJsonAsync<OmdbVideoResponseModel>(BuildQueryString(parameters))
            .ConfigureAwait(false);
    }

    private HttpClient GetClient()
    {
        var client = _httpClientFactory.CreateClient(HttpClientNames.Omdb);
        client.BaseAddress = new Uri(Options.BaseUrl);
        return client;
    }

    private string BuildQueryString(OmdbParameters parameters)
    {
        var parametersQuery = parameters.ToQueryString();
        return $"?apiKey={Options.ApiKey}&{parametersQuery}";
    }
}
