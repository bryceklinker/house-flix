using House.Flix.Core.Common.Omdb;
using House.Flix.Core.Tests.Support;
using House.Flix.Testing.Support;
using House.Flix.Testing.Support.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace House.Flix.Core.Tests.Common.Omdb;

public class OmdbClientTests
{
    private readonly OmdbOptions _options;
    private readonly FakeHttpMessageHandler _handler;
    private readonly IOmdbClient _client;

    public OmdbClientTests()
    {
        var provider = ServiceProviderFactory.Create();
        _options = provider.GetRequiredService<IOptions<OmdbOptions>>().Value;
        _handler = provider.GetRequiredService<FakeHttpClientFactory>().GetHandler("omdb");
        _client = provider.GetRequiredService<IOmdbClient>();
    }

    [Fact]
    public async Task WhenSearchingForMoviesThenQueriesOmdbForMovies()
    {
        var searchResponse = OmdbModelFactory.CreateSearchResponse();
        _handler.SetupJsonGet(
            $"{_options.BaseUrl}?apiKey={_options.ApiKey}&s=Forest",
            searchResponse
        );

        var actual = await _client.SearchAsync(new OmdbSearchParameters("Forest"));

        actual.Should().BeEquivalentTo(searchResponse);
    }

    [Fact]
    public async Task WhenGettingByIdThenQueriesOmdbForMoviesById()
    {
        var response = OmdbModelFactory.CreateMovieResponse();
        _handler.SetupJsonGet($"{_options.BaseUrl}?apiKey={_options.ApiKey}&i=653", response);

        var actual = await _client.GetById(new OmdbGetByIdParameters("653"));

        actual.Should().BeEquivalentTo(response);
    }

    [Fact]
    public async Task WhenGettingByTitleThenQueriesOmdbForMoviesByTitle()
    {
        var response = OmdbModelFactory.CreateMovieResponse();
        _handler.SetupJsonGet($"{_options.BaseUrl}?apiKey={_options.ApiKey}&t=Clerks", response);

        var actual = await _client.GetByTitle(new OmdbGetByTitleParameters("Clerks"));

        actual.Should().BeEquivalentTo(response);
    }
}
