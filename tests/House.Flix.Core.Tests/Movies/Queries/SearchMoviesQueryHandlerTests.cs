using House.Flix.Core.Movies.Queries;
using House.Flix.Core.Tests.Support;
using House.Flix.Testing.Support;

namespace House.Flix.Core.Tests.Movies.Queries;

public class SearchMoviesQueryHandlerTests : CqrsTest
{
    [Fact]
    public async Task WhenSearchingForMoviesThenReturnsFirstPageOfMovies()
    {
        await AddMany(EntityFactory.Many(() => EntityFactory.CreateMovie(), 30));

        var result = await Bus.SendQueryAsync(new SearchMoviesQuery());

        result.Items.Should().HaveCount(10);
        result.PageSize.Should().Be(10);
        result.Page.Should().Be(1);
        result.TotalCount.Should().Be(30);
    }

    [Fact]
    public async Task WhenSearchingForMoviesThenSortsAlphabetically()
    {
        await Add(EntityFactory.CreateMovie("b"));
        await Add(EntityFactory.CreateMovie("c"));
        await Add(EntityFactory.CreateMovie("a"));

        var result = await Bus.SendQueryAsync(new SearchMoviesQuery());

        result.Items[0].Title.Should().Be("a");
        result.Items[1].Title.Should().Be("b");
        result.Items[2].Title.Should().Be("c");
    }

    [Fact]
    public async Task WhenPageIsLessThanSizeThenReturnsPageSizeFromQuery()
    {
        await AddMany(EntityFactory.Many(() => EntityFactory.CreateMovie(), 3));

        var result = await Bus.SendQueryAsync(new SearchMoviesQuery(PageSize: 5));

        result.Items.Should().HaveCount(3);
        result.PageSize.Should().Be(5);
        result.TotalCount.Should().Be(3);
    }

    [Fact]
    public async Task WhenSearchingForMoviesThenLimitsResultsBasedOnTitle()
    {
        await AddMany(EntityFactory.Many(() => EntityFactory.CreateMovie(), 100));
        await AddMany(EntityFactory.Many(() => EntityFactory.CreateMovie("bill"), 30));

        var result = await Bus.SendQueryAsync(new SearchMoviesQuery("bill"));

        result.Items.Should().HaveCount(10);
        result.PageSize.Should().Be(10);
        result.Page.Should().Be(1);
        result.TotalCount.Should().Be(30);
    }

    [Fact]
    public async Task WhenPageSizeIsProvidedThenReturnsAdjustedPageSize()
    {
        await AddMany(EntityFactory.Many(() => EntityFactory.CreateMovie(), 100));

        var result = await Bus.SendQueryAsync(new SearchMoviesQuery(PageSize: 40));

        result.Items.Should().HaveCount(40);
        result.PageSize.Should().Be(40);
    }

    [Fact]
    public async Task WhenPageIsProvidedThenReturnsStartingAtPage()
    {
        await AddMany(EntityFactory.Many(() => EntityFactory.CreateMovie(), 100));

        var first = await Bus.SendQueryAsync(new SearchMoviesQuery(Page: 1, PageSize: 5));
        var second = await Bus.SendQueryAsync(new SearchMoviesQuery(Page: 3, PageSize: 5));

        second.Items[0].Id.Should().NotBe(first.Items[0].Id);
        second.Page.Should().Be(3);
    }
}
