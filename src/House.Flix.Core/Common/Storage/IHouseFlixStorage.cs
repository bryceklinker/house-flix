namespace House.Flix.Core.Common.Storage;

public interface IHouseFlixStorage
{
    IQueryable<T> Set<T>() where T : class;

    Task SaveAsync();
}
