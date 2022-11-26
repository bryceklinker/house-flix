using House.Flix.Core.Common.Http;
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
        _handler = provider
            .GetRequiredService<FakeHttpClientFactory>()
            .GetHandler(HttpClientNames.Omdb);
        _client = provider.GetRequiredService<IOmdbClient>();
    }

    [Fact]
    public async Task WhenSearchingForMoviesThenQueriesOmdbForMovies()
    {
        HttpRequestMessage? request = null;
        var searchResponse = OmdbModelFactory.CreateSearchResponse();
        _handler.SetupJsonGet(
            _options.BaseUrl,
            searchResponse,
            new SetupRequestOptions(Capture: req => request = req)
        );

        var actual = await _client.SearchAsync(new OmdbSearchParameters("Forest"));

        actual.Should().BeEquivalentTo(searchResponse);
        request!.RequestUri!.Query
            .Should()
            .Contain("s=Forest")
            .And.Contain($"apiKey={_options.ApiKey}");
    }

    [Fact]
    public async Task WhenGettingByIdThenQueriesOmdbForMoviesById()
    {
        HttpRequestMessage? request = null;
        var response = OmdbModelFactory.CreateVideoResponse();
        _handler.SetupJsonGet(
            _options.BaseUrl,
            response,
            new SetupRequestOptions(Capture: req => request = req)
        );

        var actual = await _client.GetById(new OmdbGetByIdParameters("653"));

        actual.Should().BeEquivalentTo(response);
        request!.RequestUri!.Query
            .Should()
            .Contain("i=653")
            .And.Contain($"apiKey={_options.ApiKey}");
    }

    [Fact]
    public async Task WhenGettingByTitleThenQueriesOmdbForMoviesByTitle()
    {
        HttpRequestMessage? request = null;
        var response = OmdbModelFactory.CreateVideoResponse();
        _handler.SetupJsonGet(
            $"{_options.BaseUrl}?apiKey={_options.ApiKey}&t=Clerks",
            response,
            new SetupRequestOptions(Capture: req => request = req)
        );

        var actual = await _client.GetByTitle(new OmdbGetByTitleParameters("Clerks"));

        actual.Should().BeEquivalentTo(response);
        request!.RequestUri!.Query
            .Should()
            .Contain("t=Clerks")
            .And.Contain($"apiKey={_options.ApiKey}");
    }
}
