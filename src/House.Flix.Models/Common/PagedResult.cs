namespace House.Flix.Models.Common;

public record PagedResult<T>(T[] Items, long Page, long PageSize, long TotalCount);
