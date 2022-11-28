using House.Flix.Core.Common.Entities;
using House.Flix.Core.Common.Events;
using House.Flix.Core.Common.Http;
using House.Flix.Core.Common.Omdb;
using House.Flix.Core.Movies.Entities;
using House.Flix.Core.Tests.Support;
using House.Flix.Testing.Support;
using House.Flix.Testing.Support.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace House.Flix.Core.Tests.Movies.Events;

public class MovieFileLocatedEventHandlerTests : CqrsTest
{
    private readonly string _filePath = Path.GetFullPath(
        Path.Join(Path.GetTempPath(), "Forgetting Sarah Marshall (2008).mp4")
    );
    private readonly OmdbOptions _omdbOptions;
    private readonly FakeHttpMessageHandler _omdbHandler;
    private readonly OmdbVideoResponseModel _omdbResponse;

    public MovieFileLocatedEventHandlerTests()
    {
        File.WriteAllText(_filePath, "something");
        _omdbOptions = Provider.GetRequiredService<IOptions<OmdbOptions>>().Value;

        _omdbHandler = Provider
            .GetRequiredService<FakeHttpClientFactory>()
            .GetHandler(HttpClientNames.Omdb);
        _omdbResponse = OmdbModelFactory.CreateVideoResponse() with { Type = OmdbVideoTypes.Movie };
        _omdbHandler.SetupJsonGet($"{_omdbOptions.BaseUrl}", _omdbResponse);
    }

    [Fact]
    public async Task WhenMovieLocatedThenAddsMovieToDatabase()
    {
        await Bus.PublishEventAsync(new VideoFileLocatedEvent(_filePath));

        Storage.Set<MovieEntity>().Should().HaveCount(1);
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

        await Bus.PublishEventAsync(new VideoFileLocatedEvent(_filePath));

        uri!.Query.Should().Contain("t=Forgetting+Sarah+Marshall").And.Contain("y=2008");
    }

    [Fact]
    public async Task WhenMovieLocatedThenPopulatesMovieFromOmdb()
    {
        await Bus.PublishEventAsync(new VideoFileLocatedEvent(_filePath));

        var movie = Storage.Set<MovieEntity>().Single();
        movie.Plot.Should().Be(_omdbResponse.Plot);
        movie.Rating.Should().Be(_omdbResponse.Rated);
        movie.Title.Should().Be(_omdbResponse.Title);
    }

    [Fact]
    public async Task WhenOmdbResultIsNotMovieThenNothingIsAddedToDatabase()
    {
        var response = _omdbResponse with { Type = OmdbVideoTypes.Series };
        _omdbHandler.SetupJsonGet($"{_omdbOptions.BaseUrl}", response);

        await Bus.PublishEventAsync(new VideoFileLocatedEvent(_filePath));

        Storage.Set<MovieEntity>().Should().BeEmpty();
        Storage.Set<VideoFileEntity>().Should().BeEmpty();
    }

    [Fact]
    public async Task WhenMovieLocatedThenAddsVideoToDatabase()
    {
        await Bus.PublishEventAsync(new VideoFileLocatedEvent(_filePath));

        Storage.Set<VideoFileEntity>().Should().HaveCount(1);
    }

    [Fact]
    public async Task WhenFileIsNotAnMp4ThenFileChangeIsIgnored()
    {
        var filePath = Path.GetFullPath(
            Path.Join(Path.GetTempPath(), "Forgetting Sarah Marshall (2008).txt")
        );

        await Bus.PublishEventAsync(new VideoFileLocatedEvent(filePath));

        Storage.Set<MovieEntity>().Should().BeEmpty();
        Storage.Set<VideoFileEntity>().Should().BeEmpty();
    }

    public override void Dispose()
    {
        base.Dispose();
        File.Delete(_filePath);
    }
}
