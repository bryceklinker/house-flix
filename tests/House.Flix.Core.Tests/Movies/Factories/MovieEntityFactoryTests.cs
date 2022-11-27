using House.Flix.Core.Common.Omdb;
using House.Flix.Core.Movies.Factories;
using House.Flix.Core.Tests.Support;
using House.Flix.Testing.Support;
using Microsoft.Extensions.DependencyInjection;

namespace House.Flix.Core.Tests.Movies.Factories;

public class MovieEntityFactoryTests : IDisposable
{
    private static readonly string FilePath = Path.GetTempFileName();
    private readonly IMovieEntityFactory _factory;

    public MovieEntityFactoryTests()
    {
        File.WriteAllText(FilePath, "this-would-be-an-actual-movie");
        _factory = ServiceProviderFactory.Create().GetRequiredService<IMovieEntityFactory>();
    }

    [Fact]
    public void WhenResponseIsNullThenReturnsNull()
    {
        var movie = _factory.Create(null, FilePath);
        movie.Should().BeNull();
    }

    [Theory]
    [InlineData(OmdbVideoTypes.Episode)]
    [InlineData(OmdbVideoTypes.Series)]
    public void WhenResponseIsWrongTypeThenReturnsNull(string type)
    {
        var response = OmdbModelFactory.CreateVideoResponse() with { Type = type };

        var movie = _factory.Create(response, FilePath);

        movie.Should().BeNull();
    }

    [Fact]
    public void WhenResponseIsForAMovieThenReturnsMoviePopulatedFromResponse()
    {
        var response = OmdbModelFactory.CreateVideoResponse() with { Type = OmdbVideoTypes.Movie };

        var movie = _factory.Create(response, FilePath);

        movie!.Plot.Should().Be(response.Plot);
        movie.Rating.Should().Be(response.Rated);
        movie.Title.Should().Be(response.Title);
    }

    [Fact]
    public void WhenResponseIsForMovieThenReturnsMovieWithVideoFile()
    {
        var response = OmdbModelFactory.CreateVideoResponse() with { Type = OmdbVideoTypes.Movie };

        var movie = _factory.Create(response, FilePath);

        movie!.VideoFile.Path.Should().Be(FilePath);
        movie!.VideoFile.Size.Should().Be(29);
    }

    public void Dispose()
    {
        File.Delete(FilePath);
    }
}
