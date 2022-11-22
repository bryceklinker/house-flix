using House.Flix.Core.Common.Storage;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace House.Flix.PostgreSQL;

public static class HouseFlixPostgresServiceCollectionExtensions
{
    private static readonly string? MigrationsAssembly =
        typeof(HouseFlixPostgresServiceCollectionExtensions).Assembly.GetName().Name;

    public static IServiceCollection AddHouseFlixPostgreSQL(
        this IServiceCollection services,
        string connectionString
    )
    {
        services.AddDbContext<HouseFlixContext>(opts =>
        {
            opts.UseNpgsql(
                connectionString,
                pg => pg.MigrationsAssembly(MigrationsAssembly).EnableRetryOnFailure()
            );
        });
        return services;
    }
}
