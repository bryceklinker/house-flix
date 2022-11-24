using Bogus;
using House.Flix.Core.Movies.Entities;
using House.Flix.Core.People.Entities;

namespace House.Flix.Testing.Support;

public static class EntityFactory
{
    public static MovieEntity CreateMovie()
    {
        return new Faker<MovieEntity>().Generate();
    }

    public static PersonEntity CreatePerson()
    {
        return new Faker<PersonEntity>().Generate();
    }

    public static RoleEntity CreateRole()
    {
        return new Faker<RoleEntity>().Generate();
    }
}
