using House.Flix.Core.Movies.Entities;
using House.Flix.PostgreSQL.Common;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace House.Flix.PostgreSQL.Movies;

public class MovieRoleEntityConfiguration : EntityTypeConfiguration<MovieRoleEntity>
{
    protected override void ConfigureEntity(EntityTypeBuilder<MovieRoleEntity> builder)
    {
        builder.HasOne(mr => mr.Person).WithMany(p => p.MovieRoles).IsRequired();

        builder.HasOne(mr => mr.Movie).WithMany(m => m.MovieRoles).IsRequired();

        builder.HasOne(mr => mr.Role).WithMany(r => r.MovieRoles).IsRequired();
    }
}
