using System.Reflection;
using House.Flix.Core.Common.Cqrs;
using House.Flix.Core.Common.Cqrs.Commands;
using House.Flix.Core.Common.Cqrs.Events;
using House.Flix.Core.Common.Cqrs.Queries;
using House.Flix.Core.Common.Cqrs.Shared;
using House.Flix.Core.Common.Omdb;
using House.Flix.Core.Common.Parsing;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Serilog;

namespace House.Flix.Core;

public static class HouseFlixServiceCollectionExtensions
{
    public static IServiceCollection AddHouseFlixCore(
        this IServiceCollection services,
        IConfiguration config,
        params Assembly[] assemblies
    )
    {
        services.AddHttpClient();

        services.AddLogging(b =>
        {
            b.AddSerilog();
        });
        services.Configure<OmdbOptions>(config.GetSection(OmdbOptions.Section));
        services.TryAddTransient(typeof(IPipelineBehavior<,>), typeof(CqrsLoggingMiddleware<,>));
        services.AddMediatR(
            assemblies.Append(typeof(HouseFlixServiceCollectionExtensions).Assembly).ToArray()
        );
        services.TryAddTransient<IEventBus, EventBus>();
        services.TryAddTransient<IQueryBus, QueryBus>();
        services.TryAddTransient<ICommandBus, CommandBus>();
        services.TryAddTransient<ICqrsBus, CqrsBus>();
        services.TryAddTransient<IOmdbClient, OmdbClient>();
        services.TryAddTransient<IVideoFileNameParser, VideoFileNameParser>();
        return services;
    }
}
