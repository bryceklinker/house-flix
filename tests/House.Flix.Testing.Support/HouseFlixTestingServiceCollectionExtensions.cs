using House.Flix.Core;
using House.Flix.PostgreSQL;
using House.Flix.Testing.Support.Logging;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;

namespace House.Flix.Testing.Support;

public static class HouseFlixTestingServiceCollectionExtensions
{
    public static IServiceCollection AddHouseFlixTestingSupport(
        this IServiceCollection services,
        HouseFlixTestingSupportOptions? options = null
    )
    {
        var supportOptions = options ?? new HouseFlixTestingSupportOptions();
        var loggerFactory = new FakeLoggerFactory();
        services
            .AddHouseFlixCore()
            .AddHouseFlixPostgreSql(opts => opts.UseInMemoryDatabase(supportOptions.DatabaseName));
        services.Replace<ILoggerFactory, FakeLoggerFactory>(loggerFactory);
        services.TryAddSingleton(loggerFactory);
        services.TryAddSingleton(loggerFactory.Logger);
        return services;
    }

    public static IServiceCollection Replace<TService, TInstance>(
        this IServiceCollection services,
        TInstance instance
    ) where TInstance : notnull, TService
    {
        return services.Replace(typeof(TService), instance);
    }

    public static IServiceCollection Replace(
        this IServiceCollection services,
        Type serviceType,
        object instance
    )
    {
        var descriptor = services.FirstOrDefault(s => s.ServiceType == serviceType);
        if (descriptor != null)
            services.Remove(descriptor);
        services.AddSingleton(serviceType, instance);
        return services;
    }
}
