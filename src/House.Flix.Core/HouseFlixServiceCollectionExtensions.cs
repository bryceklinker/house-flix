using System.Reflection;
using House.Flix.Core.Common.Cqrs;
using House.Flix.Core.Common.Cqrs.Commands;
using House.Flix.Core.Common.Cqrs.Events;
using House.Flix.Core.Common.Cqrs.Queries;
using House.Flix.Core.Common.Cqrs.Shared;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Serilog;

namespace House.Flix.Core;

public static class HouseFlixServiceCollectionExtensions
{
    public static IServiceCollection AddHouseFlixCore(this IServiceCollection services, params Assembly[] assemblies)
    {
        services.AddLogging(b =>
        {
            b.AddSerilog();
        });
        services.TryAddTransient(typeof(IPipelineBehavior<,>), typeof(CqrsLoggingMiddleware<,>));
        services.AddMediatR(assemblies.Append(typeof(HouseFlixServiceCollectionExtensions).Assembly).ToArray());
        services.TryAddTransient<IEventBus, EventBus>();
        services.TryAddTransient<IQueryBus, QueryBus>();
        services.TryAddTransient<ICommandBus, CommandBus>();
        services.TryAddTransient<ICqrsBus, CqrsBus>();
        return services;
    }
}