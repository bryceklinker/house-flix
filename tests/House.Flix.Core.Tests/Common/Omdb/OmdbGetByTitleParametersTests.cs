using House.Flix.Core.Common.Omdb;

namespace House.Flix.Core.Tests.Common.Omdb;

public class OmdbGetByTitleParametersTests
{
    [Fact]
    public void WhenTitleIsProvidedThenReturnsTitleInQueryString()
    {
        var parameters = new OmdbGetByTitleParameters("Forest Gump");

        parameters.ToQueryString().Should().Contain("t=Forest+Gump");
    }
}
