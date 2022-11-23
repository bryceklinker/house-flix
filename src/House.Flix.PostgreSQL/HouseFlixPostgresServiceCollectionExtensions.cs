using House.Flix.Core.Common.Storage;
using House.Flix.PostgreSQL.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace House.Flix.PostgreSQL;

public static class HouseFlixPostgresServiceCollectionExtensions
{
    private static readonly string? MigrationsAssembly =
        typeof(HouseFlixPostgresServiceCollectionExtensions).Assembly.GetName().Name;

    public static IServiceCollection AddHouseFlixPostgreSql(
        this IServiceCollection services,
        string connectionString
    )
    {
        return services.AddHouseFlixPostgreSql(opts =>
        {
            opts.UseNpgsql(
                connectionString,
                pg => pg.MigrationsAssembly(MigrationsAssembly).EnableRetryOnFailure()
            );
        });
    }

    public static IServiceCollection AddHouseFlixPostgreSql(
        this IServiceCollection services,
        Action<DbContextOptionsBuilder> configure
    )
    {
        services.AddScoped<IHouseFlixStorage, PostgresHouseFlixStorage>();
        services.AddDbContext<PostgresHouseFlixStorage>(configure);
        return services;
    }
}
