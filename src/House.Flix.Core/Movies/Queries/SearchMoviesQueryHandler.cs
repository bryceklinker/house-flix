using AutoMapper;
using AutoMapper.QueryableExtensions;
using House.Flix.Core.Common.Cqrs.Queries;
using House.Flix.Core.Common.Paging;
using House.Flix.Core.Common.Storage;
using House.Flix.Core.Movies.Entities;
using House.Flix.Models.Common;
using House.Flix.Models.Movies;

namespace House.Flix.Core.Movies.Queries;

public record SearchMoviesQuery(string Term = "", int Page = 1, int PageSize = 10)
    : PagedQuery(Page, PageSize),
        IQuery<PagedResult<MovieModel>>;

public class SearchMoviesQueryHandler : IQueryHandler<SearchMoviesQuery, PagedResult<MovieModel>>
{
    private readonly IHouseFlixStorage _storage;
    private readonly IMapper _mapper;

    public SearchMoviesQueryHandler(IHouseFlixStorage storage, IMapper mapper)
    {
        _storage = storage;
        _mapper = mapper;
    }

    public async Task<PagedResult<MovieModel>> Handle(
        SearchMoviesQuery request,
        CancellationToken cancellationToken
    )
    {
        return await _storage
            .Set<MovieEntity>()
            .Where(m => m.Title.Contains(request.Term, StringComparison.OrdinalIgnoreCase))
            .OrderBy(m => m.Title)
            .ProjectTo<MovieModel>(_mapper.ConfigurationProvider)
            .ToPagedResultAsync(request, cancellationToken: cancellationToken)
            .ConfigureAwait(false);
    }
}
