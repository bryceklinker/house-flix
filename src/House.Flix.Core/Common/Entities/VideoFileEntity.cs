namespace House.Flix.Core.Common.Entities;

public class VideoFileEntity : IEntity
{
    public Guid Id { get; set; }
    public string Path { get; set; } = "";
    public long Size { get; set; }
}
