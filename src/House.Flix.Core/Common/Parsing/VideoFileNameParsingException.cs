namespace House.Flix.Core.Common.Parsing;

public class VideoFileNameParsingException : Exception
{
    public VideoFileNameParsingException(string filePath, Exception inner)
        : base($"Unable to parse video file name from path: {filePath}", inner) { }
}
