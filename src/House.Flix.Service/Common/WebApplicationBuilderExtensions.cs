using House.Flix.Core;
using House.Flix.PostgreSQL;

namespace House.Flix.Service.Common;

public static class WebApplicationBuilderExtensions
{
    private const string HouseFlixConnectionStringName = "HouseFlix";

    public static WebApplicationBuilder AddHouseFlixService(this WebApplicationBuilder builder)
    {
        var houseFlixConnectionString = builder.Configuration.GetConnectionString(
            HouseFlixConnectionStringName
        );
        if (string.IsNullOrWhiteSpace(houseFlixConnectionString))
            throw new InvalidOperationException(
                $"You must provide a '{HouseFlixConnectionStringName}' connection string"
            );

        builder.Services
            .AddHouseFlixCore(builder.Configuration)
            .AddHouseFlixPostgreSql(houseFlixConnectionString);

        builder.Services.AddControllers();
        return builder;
    }
}
