using House.Flix.Core.Common.Entities;
using House.Flix.Core.People.Entities;

namespace House.Flix.Core.Movies.Entities;

public class MovieRoleEntity : IEntity
{
    public Guid Id { get; set; }

    public PersonEntity Person { get; set; }
    public RoleEntity Role { get; set; }
    public MovieEntity Movie { get; set; }
}
