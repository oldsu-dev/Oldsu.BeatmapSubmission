namespace Oldsu.BeatmapSubmission.Memory;

public interface ITemporaryMemoryDirectoryItem
{
    string Name { get; }
    long Size { get; }
    Stream CreateReadOnlyStream();
}

public interface ITemporaryMemoryDirectoryToken: IAsyncDisposable
{
    IEnumerable<ITemporaryMemoryDirectoryItem> Items { get; }
}