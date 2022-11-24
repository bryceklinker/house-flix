using House.Flix.Testing.Support.Http;
using House.Flix.Testing.Support.Logging;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;

namespace House.Flix.Testing.Support;

public static class HouseFlixTestingServiceCollectionExtensions
{
    public static IServiceCollection AddHouseFlixTestingSupport(
        this IServiceCollection services,
        HouseFlixTestingSupportOptions options
    )
    {
        services.TryAddSingleton(options.HttpClientFactory);
        services.TryAddSingleton(options.LoggerFactory);
        services.TryAddSingleton(options.Logger);
        services.Replace<ILoggerFactory, FakeLoggerFactory>(options.LoggerFactory);
        services.Replace<IHttpClientFactory, FakeHttpClientFactory>(options.HttpClientFactory);
        options.ConfigureServices(services);
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
