using House.Flix.PostgreSQL;
using House.Flix.Testing.Support;
using House.Flix.Testing.Support.Http;
using House.Flix.Testing.Support.Logging;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;

namespace House.Flix.Core.Tests.Support;

public static class ServiceProviderFactory
{
    public static IServiceProvider Create(HouseFlixTestingSupportOptions? options = null)
    {
        var supportOptions = options ?? new HouseFlixTestingSupportOptions();
        var configBuilder = new ConfigurationBuilder();
        supportOptions.ConfigureConfig(configBuilder);
        var config = configBuilder.Build();

        var services = new ServiceCollection()
            .AddSingleton(config)
            .AddHouseFlixTestingSupport(supportOptions)
            .AddHouseFlixCore(config, typeof(ServiceProviderFactory).Assembly)
            .AddHouseFlixPostgreSql(opts => opts.UseInMemoryDatabase(supportOptions.DatabaseName));
        return services.BuildServiceProvider();
    }
}
