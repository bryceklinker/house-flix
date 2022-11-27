using House.Flix.Core.Common.Cqrs.Events;
using House.Flix.Core.Common.Entities;
using House.Flix.Core.Common.Events;
using House.Flix.Core.Common.Omdb;
using House.Flix.Core.Common.Parsing;
using House.Flix.Core.Common.Storage;
using House.Flix.Core.Movies.Entities;
using House.Flix.Core.Movies.Factories;

namespace House.Flix.Core.Movies.Events;

public class MovieFileLocatedEventHandler : IEventHandler<VideoFileLocatedEvent>
{
    private readonly IHouseFlixStorage _storage;
    private readonly IOmdbClient _omdbClient;
    private readonly IVideoFileNameParser _parser;
    private readonly IMovieEntityFactory _factory;

    public MovieFileLocatedEventHandler(
        IHouseFlixStorage storage,
        IOmdbClient omdbClient,
        IVideoFileNameParser parser,
        IMovieEntityFactory factory
    )
    {
        _storage = storage;
        _omdbClient = omdbClient;
        _parser = parser;
        _factory = factory;
    }

    public async Task Handle(VideoFileLocatedEvent @event, CancellationToken cancellationToken)
    {
        if (Path.GetExtension(@event.FilePath) != ".mp4")
            return;

        var parts = _parser.Parse(@event.FilePath);

        var response = await _omdbClient
            .GetByTitle(new OmdbGetByTitleParameters(parts.Title, Year: parts.Year))
            .ConfigureAwait(false);

        var movie = _factory.Create(response, @event.FilePath);
        if (movie == null)
            return;

        _storage.Add(movie);
        await _storage.SaveAsync().ConfigureAwait(false);
    }
}
