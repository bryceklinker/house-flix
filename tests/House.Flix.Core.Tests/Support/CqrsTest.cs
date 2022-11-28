using House.Flix.Core.Common.Cqrs;
using House.Flix.Core.Common.Entities;
using House.Flix.Core.Common.Storage;
using House.Flix.Testing.Support;
using Microsoft.Extensions.DependencyInjection;

namespace House.Flix.Core.Tests.Support;

public class CqrsTest : IDisposable
{
    protected IServiceProvider Provider { get; }
    protected CapturingCqrsBus Bus { get; }
    protected IHouseFlixStorage Storage { get; }

    public CqrsTest(HouseFlixTestingSupportOptions? options = null)
    {
        Provider = ServiceProviderFactory.Create(options);
        Bus = (CapturingCqrsBus)Provider.GetRequiredService<ICqrsBus>();
        Storage = Provider.GetRequiredService<IHouseFlixStorage>();
    }

    public virtual void Dispose()
    {
        Storage.Dispose();
    }

    protected async Task Add<T>(T entity) where T : class, IEntity
    {
        Storage.Add(entity);
        await Storage.SaveAsync();
    }

    protected async Task AddMany<T>(params T[] entities) where T : class, IEntity
    {
        foreach (var entity in entities)
            Storage.Add(entity);
        await Storage.SaveAsync();
    }
}
