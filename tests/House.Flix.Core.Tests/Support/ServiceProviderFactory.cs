using House.Flix.Testing.Support;
using Microsoft.Extensions.DependencyInjection;

namespace House.Flix.Core.Tests.Support;

public static class ServiceProviderFactory
{
    public static IServiceProvider Create(Action<IServiceCollection>? configureServices = null)
    {
        var services = new ServiceCollection();
        services
            .AddHouseFlixCore(typeof(ServiceProviderFactory).Assembly)
            .AddHouseFlixTestingSupport();

        configureServices?.Invoke(services);
        return services.BuildServiceProvider();
    }
}