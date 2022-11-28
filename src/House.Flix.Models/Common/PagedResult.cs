namespace House.Flix.Models.Common;

public record PagedResult<T>(T[] Items, int Page, int PageSize, int TotalCount);
