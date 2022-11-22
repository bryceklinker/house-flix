using House.Flix.Core.People.Entities;
using House.Flix.PostgreSQL.Common;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace House.Flix.PostgreSQL.People;

public class PersonEntityConfiguration : EntityTypeConfiguration<PersonEntity>
{
    protected override void ConfigureEntity(EntityTypeBuilder<PersonEntity> builder)
    {
        builder.Property(p => p.FirstName).IsRequired();
        builder.Property(p => p.LastName).IsRequired();

        builder.HasMany(p => p.MovieRoles).WithOne(mr => mr.Person).IsRequired();
    }
}
