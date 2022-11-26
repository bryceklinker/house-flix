using House.Flix.Core.Common.Entities;
using House.Flix.Core.Common.Storage;
using Microsoft.EntityFrameworkCore;

namespace House.Flix.PostgreSQL.Common;

public class PostgresHouseFlixStorage : DbContext, IHouseFlixStorage
{
    public PostgresHouseFlixStorage(DbContextOptions<PostgresHouseFlixStorage> options)
        : base(options) { }

    IQueryable<T> IHouseFlixStorage.Set<T>() where T : class
    {
        return Set<T>();
    }

    void IHouseFlixStorage.Add<T>(T entity) where T : class
    {
        Add(entity);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(PostgresHouseFlixStorage).Assembly);
        base.OnModelCreating(modelBuilder);
    }

    public async Task SaveAsync()
    {
        await SaveChangesAsync();
    }
}
