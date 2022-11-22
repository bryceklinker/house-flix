using House.Flix.Core.People.Entities;
using House.Flix.PostgreSQL.Common;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace House.Flix.PostgreSQL.People;

public class RoleEntityConfiguration : EntityTypeConfiguration<RoleEntity>
{
    protected override void ConfigureEntity(EntityTypeBuilder<RoleEntity> builder)
    {
        builder.Property(r => r.Name).IsRequired();

        builder.HasMany(r => r.MovieRoles).WithOne(mr => mr.Role).IsRequired();
    }
}
