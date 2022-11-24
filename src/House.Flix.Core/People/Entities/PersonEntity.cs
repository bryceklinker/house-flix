using House.Flix.Core.Common.Entities;
using House.Flix.Core.Movies.Entities;

namespace House.Flix.Core.People.Entities;

public class PersonEntity : IEntity
{
    public Guid Id { get; set; }

    public string FirstName { get; set; } = "";
    public string LastName { get; set; } = "";

    public List<MovieRoleEntity> MovieRoles { get; set; } = new();
}
