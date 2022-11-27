using House.Flix.Core.Common.Entities;
using House.Flix.Core.Common.Omdb;
using House.Flix.Core.Movies.Entities;

namespace House.Flix.Core.Movies.Factories;

public interface IMovieEntityFactory
{
    MovieEntity? Create(OmdbVideoResponseModel? model, string filePath);
}

internal class MovieEntityFactory : IMovieEntityFactory
{
    public MovieEntity? Create(OmdbVideoResponseModel? model, string filePath)
    {
        if (model is not { Type: OmdbVideoTypes.Movie })
            return null;

        return new MovieEntity
        {
            Plot = model.Plot,
            Rating = model.Rated,
            Title = model.Title,
            VideoFile = new VideoFileEntity
            {
                Path = filePath,
                Size = new FileInfo(filePath).Length
            }
        };
    }
}
