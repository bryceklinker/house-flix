using Microsoft.Extensions.Logging;

namespace House.Flix.Core.Common.Parsing;

public record VideoFileParts(string Title, int Year);

public interface IVideoFileNameParser
{
    VideoFileParts Parse(string filePath);
}

internal class VideoFileNameParser : IVideoFileNameParser
{
    private readonly ILogger<VideoFileNameParser> _logger;

    public VideoFileNameParser(ILogger<VideoFileNameParser> logger)
    {
        _logger = logger;
    }

    public VideoFileParts Parse(string filePath)
    {
        try
        {
            var filename = Path.GetFileNameWithoutExtension(filePath);
            var title = GetTitleFromFilePath(filename);
            var year = GetYearFromFilePath(filename);
            return new VideoFileParts(title, year);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Failed to parse {FilePath}", filePath);
            throw new VideoFileNameParsingException(filePath, e);
        }
    }

    private static string GetTitleFromFilePath(string filename)
    {
        return filename.Split("(")[0].Trim();
    }

    private static int GetYearFromFilePath(string filename)
    {
        var yearPart = filename.Split("(")[1].Trim();
        return int.TryParse(yearPart.Replace(")", ""), out var result) ? result : -1;
    }
}
