using Bogus;
using House.Flix.Core.Common.Omdb;

namespace House.Flix.Testing.Support;

public static class OmdbModelFactory
{
    public static OmdbSearchResponseModel CreateSearchResponse(int count = 3)
    {
        var faker = new Faker();
        var search = Enumerable
            .Range(0, count)
            .Select(
                _ =>
                    new OmdbSearchMovieModel(
                        faker.Random.AlphaNumeric(6),
                        faker.Name.JobTitle(),
                        faker.Date.PastDateOnly().Year.ToString(),
                        faker.PickRandom("movie", "series"),
                        faker.Internet.Url()
                    )
            )
            .ToArray();
        return new OmdbSearchResponseModel("True", $"{count}", search);
    }

    public static OmdbMovieResponseModel CreateMovieResponse()
    {
        var faker = new Faker();
        return new OmdbMovieResponseModel(
            faker.Name.JobTitle(),
            faker.Date.PastDateOnly().Year.ToString(),
            faker.PickRandom("PG-13", "R", "N/A"),
            faker.Date.PastDateOnly().ToString(),
            $"{faker.Random.Number(60, 240)} min",
            faker.PickRandom("Comedy", "Horror"),
            faker.Name.FullName(),
            faker.Name.FullName(),
            string.Join(
                ", ",
                Enumerable.Range(0, faker.Random.Number(6)).Select(_ => faker.Name.FullName())
            ),
            faker.Lorem.Paragraph(),
            "English",
            faker.Address.Country(),
            "",
            faker.Internet.Url(),
            "",
            "",
            faker.Random.AlphaNumeric(6),
            faker.PickRandom("movie", "series"),
            faker.Date.PastDateOnly().ToString(),
            "",
            "",
            faker.Internet.Url(),
            "True",
            Array.Empty<string>()
        );
    }

    public static OmdbErrorResponseModel CreateErrorResponse(string? message = null)
    {
        return new OmdbErrorResponseModel("False", message ?? "An error has occurred");
    }
}
