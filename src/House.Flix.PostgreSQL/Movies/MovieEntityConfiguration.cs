using House.Flix.Core.Movies.Entities;
using House.Flix.PostgreSQL.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace House.Flix.PostgreSQL.Movies;

public class MovieEntityConfiguration : EntityTypeConfiguration<MovieEntity>
{
    protected override void ConfigureEntity(EntityTypeBuilder<MovieEntity> builder)
    {
        builder.Property(m => m.Plot).IsRequired().HasDefaultValueSql("");
        builder.Property(m => m.Rating).IsRequired().HasDefaultValueSql("");
        builder.Property(m => m.Title).IsRequired().HasDefaultValueSql("");
        builder.HasOne(m => m.VideoFile);

        builder.HasMany(m => m.MovieRoles).WithOne(mr => mr.Movie).IsRequired();
    }
}
