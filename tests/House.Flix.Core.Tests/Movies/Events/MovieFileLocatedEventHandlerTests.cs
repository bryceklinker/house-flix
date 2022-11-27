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

public class MovieFileLocatedEventHandlerTests : IDisposable
{
    private readonly string _filePath = Path.GetFullPath(
        Path.Join(Path.GetTempPath(), "Forgetting Sarah Marshall (2008).mp4")
    );
    private readonly ICqrsBus _bus;
    private readonly IHouseFlixStorage _storage;
    private readonly OmdbOptions _omdbOptions;
    private readonly FakeHttpMessageHandler _omdbHandler;
    private readonly OmdbVideoResponseModel _omdbResponse;

    public MovieFileLocatedEventHandlerTests()
    {
        File.WriteAllText(_filePath, "something");
        var provider = ServiceProviderFactory.Create();
        _omdbOptions = provider.GetRequiredService<IOptions<OmdbOptions>>().Value;

        _omdbHandler = provider
            .GetRequiredService<FakeHttpClientFactory>()
            .GetHandler(HttpClientNames.Omdb);
        _omdbResponse = OmdbModelFactory.CreateVideoResponse() with { Type = OmdbVideoTypes.Movie };
        _omdbHandler.SetupJsonGet($"{_omdbOptions.BaseUrl}", _omdbResponse);

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
            _omdbResponse,
            new SetupRequestOptions(Capture: req => uri = req.RequestUri)
        );

        await _bus.PublishEventAsync(new VideoFileLocatedEvent(_filePath));

        uri!.Query.Should().Contain("t=Forgetting+Sarah+Marshall").And.Contain("y=2008");
    }

    [Fact]
    public async Task WhenMovieLocatedThenPopulatesMovieFromOmdb()
    {
        await _bus.PublishEventAsync(new VideoFileLocatedEvent(_filePath));

        var movie = _storage.Set<MovieEntity>().Single();
        movie.Plot.Should().Be(_omdbResponse.Plot);
        movie.Rating.Should().Be(_omdbResponse.Rated);
        movie.Title.Should().Be(_omdbResponse.Title);
    }

    [Fact]
    public async Task WhenOmdbResultIsNotMovieThenNothingIsAddedToDatabase()
    {
        var response = _omdbResponse with { Type = OmdbVideoTypes.Series };
        _omdbHandler.SetupJsonGet($"{_omdbOptions.BaseUrl}", response);

        await _bus.PublishEventAsync(new VideoFileLocatedEvent(_filePath));

        _storage.Set<MovieEntity>().Should().BeEmpty();
        _storage.Set<VideoFileEntity>().Should().BeEmpty();
    }

    [Fact]
    public async Task WhenMovieLocatedThenAddsVideoToDatabase()
    {
        await _bus.PublishEventAsync(new VideoFileLocatedEvent(_filePath));

        _storage.Set<VideoFileEntity>().Should().HaveCount(1);
    }

    [Fact]
    public async Task WhenFileIsNotAnMp4ThenFileChangeIsIgnored()
    {
        var filePath = Path.GetFullPath(
            Path.Join(Path.GetTempPath(), "Forgetting Sarah Marshall (2008).txt")
        );

        await _bus.PublishEventAsync(new VideoFileLocatedEvent(filePath));

        _storage.Set<MovieEntity>().Should().BeEmpty();
        _storage.Set<VideoFileEntity>().Should().BeEmpty();
    }

    public void Dispose()
    {
        File.Delete(_filePath);
    }
}
