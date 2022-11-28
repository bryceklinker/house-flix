using Bogus;
using House.Flix.Core.Common.Entities;
using House.Flix.Core.Movies.Entities;
using House.Flix.Core.People.Entities;

namespace House.Flix.Testing.Support;

public static class EntityFactory
{
    public static MovieEntity CreateMovie(string title = "")
    {
        var faker = new Faker<MovieEntity>();
        if (!string.IsNullOrWhiteSpace(title))
            faker.RuleFor(r => r.Title, title);
        return faker.Generate();
    }

    public static PersonEntity CreatePerson()
    {
        return new Faker<PersonEntity>().Generate();
    }

    public static RoleEntity CreateRole()
    {
        return new Faker<RoleEntity>().Generate();
    }

    public static T[] Many<T>(Func<T> factory, int count = 3) where T : class, IEntity
    {
        return Enumerable.Range(0, count).Select(_ => factory()).ToArray();
    }
}
