namespace House.Flix.Core.Common.Omdb;

public static class OmdbDefaults
{
    public const string DefaultResultType = "json";
    public const string DefaultType = "";
    public const string DefaultPlot = "full";
    public const int DefaultYear = 0;
    public const int DefaultPage = 1;
}

public record OmdbResponseModel(string Response);

public record OmdbErrorResponseModel(string Response, string Error) : OmdbResponseModel(Response);

public record OmdbSearchMovieModel(
    string ImdbID,
    string Title,
    string Year,
    string Type,
    string Poster
);

public record OmdbSearchResponseModel(
    string Response,
    string TotalResults,
    OmdbSearchMovieModel[] Search
) : OmdbResponseModel(Response);

public record OmdbParameters(
    string Type = OmdbDefaults.DefaultType,
    int Year = OmdbDefaults.DefaultYear,
    string ResultType = OmdbDefaults.DefaultResultType
);

public record OmdbGetParameters(
    string Plot = OmdbDefaults.DefaultPlot,
    string Type = OmdbDefaults.DefaultType,
    int Year = OmdbDefaults.DefaultYear,
    string ResultType = OmdbDefaults.DefaultResultType
) : OmdbParameters(Type, Year, ResultType);

public record OmdbGetByTitleParameters(
    string Title,
    string Plot = OmdbDefaults.DefaultPlot,
    string Type = OmdbDefaults.DefaultType,
    int Year = OmdbDefaults.DefaultYear,
    string ResultType = OmdbDefaults.DefaultResultType
) : OmdbGetParameters(Plot, Type, Year, ResultType);

public record OmdbGetByIdParameters(
    string ImdbId,
    string Plot = OmdbDefaults.DefaultPlot,
    string Type = OmdbDefaults.DefaultType,
    int Year = OmdbDefaults.DefaultYear,
    string ResultType = OmdbDefaults.DefaultResultType
) : OmdbGetParameters(Plot, Type, Year, ResultType);

public record OmdbSearchParameters(
    string Term,
    int Page = OmdbDefaults.DefaultPage,
    string Type = OmdbDefaults.DefaultType,
    int Year = OmdbDefaults.DefaultYear,
    string ResultType = OmdbDefaults.DefaultResultType
) : OmdbParameters(Type, Year, ResultType);

public record OmdbMovieResponseModel(
    string Title,
    string Year,
    string Rated,
    string Released,
    string Runtime,
    string Genre,
    string Director,
    string Writer,
    string Actors,
    string Plot,
    string Language,
    string Country,
    string Awards,
    string Poster,
    string Metascore,
    string ImdbRating,
    string ImdbID,
    string Type,
    string DVD,
    string BoxOffice,
    string Production,
    string Website,
    string Response,
    string[] Ratings
) : OmdbResponseModel(Response);
