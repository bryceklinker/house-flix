using House.Flix.Testing.Support;

namespace House.Flix.Core.Tests.Movies.Entities;

public class MovieEntityTests
{
    [Fact]
    public void WhenPersonAddedToMovieThenRoleIsAddedToMovie()
    {
        var movie = EntityFactory.CreateMovie();
        var person = EntityFactory.CreatePerson();
        var role = EntityFactory.CreateRole();
        movie.AddRole(person, role);

        movie.MovieRoles.Should().HaveCount(1);
        movie.MovieRoles[0].Movie.Should().Be(movie);
        movie.MovieRoles[0].Person.Should().Be(person);
        movie.MovieRoles[0].Role.Should().Be(role);
    }
}
