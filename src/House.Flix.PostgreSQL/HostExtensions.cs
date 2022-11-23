using House.Flix.PostgreSQL.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace House.Flix.PostgreSQL;

public static class HostExtensions
{
    public static async Task ApplyMigrationsAsync(this IHost host)
    {
        await using var scope = host.Services.CreateAsyncScope();
        await using var context =
            scope.ServiceProvider.GetRequiredService<PostgresHouseFlixStorage>();
        await context.Database.MigrateAsync().ConfigureAwait(false);
    }
}
