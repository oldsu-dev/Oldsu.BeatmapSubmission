namespace Oldsu.BeatmapSubmission.Memory;

public class DirectoryItem(string name, Func<Stream> streamFactory)
{
    public string Name { get; } = name;
    public Func<Stream> StreamFactory { get; } = streamFactory;
}

public interface ITemporaryMemory
{
    Task<ITemporaryMemoryFileToken> CreateFileFromStreamAsync(
        Stream stream, CancellationToken cancellationToken = default);

    Task<ITemporaryMemoryDirectoryToken> CreateDirectoryFromItemsAsync(
        IEnumerable<DirectoryItem> items, CancellationToken cancellationToken = default);
}