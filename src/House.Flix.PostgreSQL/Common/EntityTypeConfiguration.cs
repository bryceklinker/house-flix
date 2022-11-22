using House.Flix.Core.Common.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace House.Flix.PostgreSQL.Common;

public abstract class EntityTypeConfiguration<T> : IEntityTypeConfiguration<T>
    where T : class, IEntity
{
    public void Configure(EntityTypeBuilder<T> builder)
    {
        builder.HasKey(e => e.Id);
        builder
            .Property(e => e.Id)
            .UseIdentityAlwaysColumn()
            .HasDefaultValueSql("gen_random_uuid()");
    }

    protected abstract void ConfigureEntity(EntityTypeBuilder<T> builder);
}
