using House.Flix.Core.Common.Omdb;

namespace House.Flix.Core.Tests.Common.Omdb;

public class OmdbGetParametersTests
{
    [Fact]
    public void WhenAllParametersAreProvidedThenParametersAreIncludedInQueryString()
    {
        var parameters = new OmdbGetParameters("short", "series", 2010, "xml");

        parameters
            .ToQueryString()
            .Should()
            .Contain("plot=short")
            .And.Contain("type=series")
            .And.Contain("y=2010")
            .And.Contain("r=xml");
    }

    [Fact]
    public void WhenNoParametersAreProvidedThenDefaultParametersAreUsed()
    {
        var parameters = new OmdbGetParameters();

        parameters
            .ToQueryString()
            .Should()
            .Contain("plot=full")
            .And.Contain("r=json")
            .And.NotContain("type=")
            .And.NotContain("y=");
    }
}
