using System.Globalization;
using System.Net;

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

public record OmdbSearchVideoModel(
    string ImdbID,
    string Title,
    string Year,
    string Type,
    string Poster
);

public record OmdbSearchResponseModel(
    string Response,
    string TotalResults,
    OmdbSearchVideoModel[] Search
) : OmdbResponseModel(Response);

public record OmdbParameters(
    string Type = OmdbDefaults.DefaultType,
    int Year = OmdbDefaults.DefaultYear,
    string ResultType = OmdbDefaults.DefaultResultType
)
{
    public string ToQueryString()
    {
        var values = GetValues().Select(kv => $"{kv.Key}={WebUtility.UrlEncode(kv.Value)}");
        return string.Join("&", values);
    }

    protected virtual Dictionary<string, string> GetValues()
    {
        var values = new Dictionary<string, string>();
        if (!string.IsNullOrWhiteSpace(Type))
            values.Add("type", Type);
        if (Year > OmdbDefaults.DefaultYear)
            values.Add("y", Year.ToString(CultureInfo.InvariantCulture));
        values.Add("r", ResultType);
        return values;
    }
}

public record OmdbGetParameters(
    string Plot = OmdbDefaults.DefaultPlot,
    string Type = OmdbDefaults.DefaultType,
    int Year = OmdbDefaults.DefaultYear,
    string ResultType = OmdbDefaults.DefaultResultType
) : OmdbParameters(Type, Year, ResultType)
{
    protected override Dictionary<string, string> GetValues()
    {
        var values = base.GetValues();
        values.Add("plot", Plot);
        return values;
    }
}

public record OmdbGetByTitleParameters(
    string Title,
    string Plot = OmdbDefaults.DefaultPlot,
    string Type = OmdbDefaults.DefaultType,
    int Year = OmdbDefaults.DefaultYear,
    string ResultType = OmdbDefaults.DefaultResultType
) : OmdbGetParameters(Plot, Type, Year, ResultType)
{
    protected override Dictionary<string, string> GetValues()
    {
        var values = base.GetValues();
        values.Add("t", Title);
        return values;
    }
}

public record OmdbGetByIdParameters(
    string ImdbId,
    string Plot = OmdbDefaults.DefaultPlot,
    string Type = OmdbDefaults.DefaultType,
    int Year = OmdbDefaults.DefaultYear,
    string ResultType = OmdbDefaults.DefaultResultType
) : OmdbGetParameters(Plot, Type, Year, ResultType)
{
    protected override Dictionary<string, string> GetValues()
    {
        var values = base.GetValues();
        values.Add("i", ImdbId);
        return values;
    }
}

public record OmdbSearchParameters(
    string Term,
    int Page = OmdbDefaults.DefaultPage,
    string Type = OmdbDefaults.DefaultType,
    int Year = OmdbDefaults.DefaultYear,
    string ResultType = OmdbDefaults.DefaultResultType
) : OmdbParameters(Type, Year, ResultType)
{
    protected override Dictionary<string, string> GetValues()
    {
        var values = base.GetValues();
        values.Add("s", Term);
        values.Add("page", Page.ToString(CultureInfo.InvariantCulture));
        return values;
    }
}

public record OmdbVideoResponseModel(
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
