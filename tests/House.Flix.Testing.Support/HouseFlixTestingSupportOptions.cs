using House.Flix.Core.Common.Omdb;
using House.Flix.Testing.Support.Http;
using House.Flix.Testing.Support.Logging;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace House.Flix.Testing.Support;

public record HouseFlixTestingSupportOptions(
    string? DatabaseName = null,
    Action<IConfigurationBuilder>? ConfigureConfig = null,
    Action<IServiceCollection>? ConfigureServices = null,
    FakeLoggerFactory? LoggerFactory = null,
    FakeHttpClientFactory? HttpClientFactory = null
)
{
    public string DatabaseName { get; } = DatabaseName ?? $"{Guid.NewGuid()}";
    public Action<IConfigurationBuilder> ConfigureConfig { get; } =
        builder => SetupDefaultConfig(builder, ConfigureConfig);
    public Action<IServiceCollection> ConfigureServices { get; } = ConfigureServices ?? (_ => { });
    public FakeLoggerFactory LoggerFactory { get; } = LoggerFactory ?? new FakeLoggerFactory();
    public FakeHttpClientFactory HttpClientFactory { get; } =
        HttpClientFactory ?? new FakeHttpClientFactory();
    public FakeLogger Logger => LoggerFactory.Logger;

    private static void SetupDefaultConfig(
        IConfigurationBuilder builder,
        Action<IConfigurationBuilder>? configure = null
    )
    {
        builder.AddInMemoryCollection(
            new[]
            {
                new KeyValuePair<string, string?>(
                    $"{OmdbOptions.Section}:{nameof(OmdbOptions.BaseUrl)}",
                    "https://omdb.com"
                ),
                new KeyValuePair<string, string?>(
                    $"{OmdbOptions.Section}:{nameof(OmdbOptions.ApiKey)}",
                    "123456789"
                ),
            }
        );
        configure?.Invoke(builder);
    }
}
