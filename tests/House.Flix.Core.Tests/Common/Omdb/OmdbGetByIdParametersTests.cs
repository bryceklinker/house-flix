using House.Flix.Core.Common.Omdb;

namespace House.Flix.Core.Tests.Common.Omdb;

public class OmdbGetByIdParametersTests
{
    [Fact]
    public void WhenParametersAreProvidedThenReturnsIdInQueryString()
    {
        var parameters = new OmdbGetByIdParameters("888");

        parameters.ToQueryString().Should().Contain("i=888");
    }
}
