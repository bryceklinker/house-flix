using House.Flix.Core.Common.Cqrs;
using House.Flix.Core.Common.Cqrs.Commands;
using House.Flix.Core.Common.Cqrs.Events;
using House.Flix.Core.Common.Cqrs.Queries;
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
            .AddHouseFlixPostgreSql(opts => opts.UseInMemoryDatabase(supportOptions.DatabaseName))
            .AddSingleton<ICqrsBus>(p =>
            {
                var eventBus = p.GetRequiredService<IEventBus>();
                var queryBus = p.GetRequiredService<IQueryBus>();
                var commandBus = p.GetRequiredService<ICommandBus>();
                var innerBus = new CqrsBus(commandBus, queryBus, eventBus);
                return new CapturingCqrsBus(innerBus);
            });
        return services.BuildServiceProvider();
    }
}
