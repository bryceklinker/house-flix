using House.Flix.Core.Common.Entities;
using House.Flix.Core.People.Entities;

namespace House.Flix.Core.Movies.Entities;

public class MovieEntity : IEntity
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Plot { get; set; }
    public string Rating { get; set; }
    public string VideoFilePath { get; set; }

    public List<MovieRoleEntity> MovieRoles { get; set; } = new();

    public void AddRole(PersonEntity person, RoleEntity role)
    {
        MovieRoles.Add(
            new MovieRoleEntity
            {
                Movie = this,
                Person = person,
                Role = role
            }
        );
    }
}
