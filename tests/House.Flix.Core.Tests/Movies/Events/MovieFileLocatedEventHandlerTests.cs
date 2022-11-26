using House.Flix.Core.Common.Cqrs;
using House.Flix.Core.Common.Entities;
using House.Flix.Core.Common.Events;
using House.Flix.Core.Common.Http;
using House.Flix.Core.Common.Omdb;
using House.Flix.Core.Common.Storage;
using House.Flix.Core.Movies.Entities;
using House.Flix.Core.Tests.Support;
using House.Flix.Testing.Support;
using House.Flix.Testing.Support.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace House.Flix.Core.Tests.Movies.Events;

public class MovieFileLocatedEventHandlerTests
{
    private readonly string _filePath = Path.GetFullPath(
        Path.Join(Directory.GetCurrentDirectory(), "Forgetting Sarah Marshall (2008).mp4")
    );
    private readonly ICqrsBus _bus;
    private readonly IHouseFlixStorage _storage;
    private readonly OmdbOptions _omdbOptions;
    private readonly FakeHttpMessageHandler _omdbHandler;

    public MovieFileLocatedEventHandlerTests()
    {
        var provider = ServiceProviderFactory.Create();
        _omdbOptions = provider.GetRequiredService<IOptions<OmdbOptions>>().Value;

        _omdbHandler = provider
            .GetRequiredService<FakeHttpClientFactory>()
            .GetHandler(HttpClientNames.Omdb);
        _omdbHandler.SetupJsonGet(
            $"{_omdbOptions.BaseUrl}",
            OmdbModelFactory.CreateVideoResponse()
        );

        _storage = provider.GetRequiredService<IHouseFlixStorage>();
        _bus = provider.GetRequiredService<ICqrsBus>();
    }

    [Fact]
    public async Task WhenMovieLocatedThenAddsMovieToDatabase()
    {
        await _bus.PublishEventAsync(new VideoFileLocatedEvent(_filePath));

        _storage.Set<MovieEntity>().Should().HaveCount(1);
    }

    [Fact]
    public async Task WhenMovieLocatedThenGetsVideoFromOmdb()
    {
        Uri? uri = null;
        _omdbHandler.SetupJsonGet(
            $"{_omdbOptions.BaseUrl}",
            OmdbModelFactory.CreateVideoResponse(),
            new SetupRequestOptions(Capture: req => uri = req.RequestUri)
        );

        await _bus.PublishEventAsync(new VideoFileLocatedEvent(_filePath));

        uri!.Query.Should().Contain("t=Forgetting+Sarah+Marshall").And.Contain("y=2008");
    }

    [Fact]
    public async Task WhenMovieLocatedThenPopulatesMovieFromOmdb()
    {
        var response = OmdbModelFactory.CreateVideoResponse();
        _omdbHandler.SetupJsonGet($"{_omdbOptions.BaseUrl}", response);

        await _bus.PublishEventAsync(new VideoFileLocatedEvent(_filePath));

        var movie = _storage.Set<MovieEntity>().Single();
        movie.Plot.Should().Be(response.Plot);
        movie.Rating.Should().Be(response.Rated);
        movie.Title.Should().Be(response.Title);
    }

    [Fact]
    public async Task WhenMovieLocatedThenAddsVideoToDatabase()
    {
        await _bus.PublishEventAsync(new VideoFileLocatedEvent(_filePath));

        _storage.Set<VideoFileEntity>().Should().HaveCount(1);
    }
}
