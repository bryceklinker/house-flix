namespace House.Flix.Testing.Support;

public record HouseFlixTestingSupportOptions(string? DatabaseName = null)
{
    public string DatabaseName { get; } = DatabaseName ?? $"{Guid.NewGuid()}";
}
