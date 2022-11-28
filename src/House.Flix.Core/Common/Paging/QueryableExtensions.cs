using House.Flix.Models.Common;
using Microsoft.EntityFrameworkCore;

namespace House.Flix.Core.Common.Paging;

public static class QueryableExtensions
{
    public static async Task<PagedResult<T>> ToPagedResultAsync<T>(
        this IQueryable<T> source,
        PagedQuery query,
        CancellationToken cancellationToken = default
    )
    {
        var skipCount = (query.Page - 1) * query.PageSize;
        var items = await source
            .Skip(skipCount)
            .Take(query.PageSize)
            .ToArrayAsync(cancellationToken)
            .ConfigureAwait(false);
        var count = await source.CountAsync(cancellationToken).ConfigureAwait(false);
        return new PagedResult<T>(items, query.Page, query.PageSize, count);
    }
}
