using House.Flix.Core.Common.Omdb;

namespace House.Flix.Core.Tests.Common.Omdb;

public class OmdbParametersTests
{
    [Fact]
    public void WhenQueryStringIsCreatedThenReturnsValuesInQueryString()
    {
        var parameters = new OmdbParameters("movie", 2019, "xml");

        parameters
            .ToQueryString()
            .Should()
            .Contain("type=movie")
            .And.Contain("y=2019")
            .And.Contain("r=xml");
    }

    [Fact]
    public void WhenValueIsMissingThenValueIsMissingFromQueryString()
    {
        var parameters = new OmdbParameters();

        parameters
            .ToQueryString()
            .Should()
            .Contain("r=json")
            .And.NotContain("type=")
            .And.NotContain("y=");
    }
}
