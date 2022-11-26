using House.Flix.Core.Common.Entities;

namespace House.Flix.Core.Common.Storage;

public interface IHouseFlixStorage
{
    IQueryable<T> Set<T>() where T : class, IEntity;

    void Add<T>(T entity) where T : class, IEntity;

    Task SaveAsync();
}
