using House.Flix.Core.Common.Cqrs.Events;
using House.Flix.Core.Common.Entities;
using House.Flix.Core.Common.Events;
using House.Flix.Core.Common.Omdb;
using House.Flix.Core.Common.Parsing;
using House.Flix.Core.Common.Storage;
using House.Flix.Core.Movies.Entities;

namespace House.Flix.Core.Movies.Events;

public class MovieFileLocatedEventHandler : IEventHandler<VideoFileLocatedEvent>
{
    private readonly IHouseFlixStorage _storage;
    private readonly IOmdbClient _omdbClient;
    private readonly IVideoFileNameParser _parser;

    public MovieFileLocatedEventHandler(
        IHouseFlixStorage storage,
        IOmdbClient omdbClient,
        IVideoFileNameParser parser
    )
    {
        _storage = storage;
        _omdbClient = omdbClient;
        _parser = parser;
    }

    public async Task Handle(VideoFileLocatedEvent @event, CancellationToken cancellationToken)
    {
        var parts = _parser.Parse(@event.FilePath);
        var response = await _omdbClient
            .GetByTitle(new OmdbGetByTitleParameters(parts.Title, Year: parts.Year))
            .ConfigureAwait(false);
        _storage.Add(
            new MovieEntity
            {
                Plot = response.Plot,
                Rating = response.Rated,
                Title = response.Title,
                VideoFile = new VideoFileEntity { Path = @event.FilePath }
            }
        );
        await _storage.SaveAsync().ConfigureAwait(false);
    }
}
