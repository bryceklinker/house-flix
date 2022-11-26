using House.Flix.Core.Common.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace House.Flix.PostgreSQL.Common;

public class VideoFileEntityConfiguration : EntityTypeConfiguration<VideoFileEntity>
{
    protected override void ConfigureEntity(EntityTypeBuilder<VideoFileEntity> builder)
    {
        builder.Property(v => v.Path).IsRequired();
        builder.Property(v => v.Size).IsRequired();
    }
}
