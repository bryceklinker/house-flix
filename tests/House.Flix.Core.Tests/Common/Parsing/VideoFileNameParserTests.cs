using House.Flix.Core.Common.Parsing;
using House.Flix.Core.Tests.Support;
using Microsoft.Extensions.DependencyInjection;

namespace House.Flix.Core.Tests.Common.Parsing;

public class VideoFileNameParserTests
{
    private readonly IVideoFileNameParser _parser;

    public VideoFileNameParserTests()
    {
        _parser = ServiceProviderFactory.Create().GetRequiredService<IVideoFileNameParser>();
    }

    [Fact]
    public void WhenParsingFileNameThenReturnsTitleAndYearFromFileName()
    {
        var parts = _parser.Parse("Forgetting Sarah Marshall (2008).mp4");
        parts.Title.Should().Be("Forgetting Sarah Marshall");
        parts.Year.Should().Be(2008);
    }

    [Fact]
    public void WhenParsingFullFilePathThenReturnsTitleAndYearFromFileName()
    {
        var path = Path.GetFullPath(
            Path.Join(Directory.GetCurrentDirectory(), "Forgetting Sarah Marshall (2008).mp4")
        );
        var parts = _parser.Parse(path);
        parts.Title.Should().Be("Forgetting Sarah Marshall");
        parts.Year.Should().Be(2008);
    }

    [Fact]
    public void WhenParsingInvalidFilePathThenThrowsingParsingException()
    {
        var act = () => _parser.Parse("");
        act.Should().Throw<VideoFileNameParsingException>();
    }
}
