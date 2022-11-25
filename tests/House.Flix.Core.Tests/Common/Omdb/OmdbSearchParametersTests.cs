using House.Flix.Core.Common.Omdb;

namespace House.Flix.Core.Tests.Common.Omdb;

public class OmdbSearchParametersTests
{
    [Fact]
    public void WhenSearchParametersProvidedThenReturnsParametersInQueryString()
    {
        var parameters = new OmdbSearchParameters("terms", 6, "movie", 2010, "xml");

        parameters
            .ToQueryString()
            .Should()
            .Contain("s=terms")
            .And.Contain("page=6")
            .And.Contain("type=movie")
            .And.Contain("y=2010")
            .And.Contain("r=xml");
    }
}
