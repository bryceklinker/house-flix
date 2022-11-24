using House.Flix.Core.Common.Entities;
using House.Flix.Core.Movies.Entities;

namespace House.Flix.Core.People.Entities;

public class RoleEntity : IEntity
{
    public Guid Id { get; set; }
    public string Name { get; set; } = "";

    public List<MovieRoleEntity> MovieRoles { get; set; } = new();
}
