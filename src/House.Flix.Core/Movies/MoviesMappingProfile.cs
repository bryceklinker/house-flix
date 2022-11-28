using AutoMapper;
using House.Flix.Core.Movies.Entities;
using House.Flix.Models.Movies;

namespace House.Flix.Core.Movies;

public class MoviesMappingProfile : Profile
{
    public MoviesMappingProfile()
    {
        CreateMap<MovieEntity, MovieModel>();
    }
}
