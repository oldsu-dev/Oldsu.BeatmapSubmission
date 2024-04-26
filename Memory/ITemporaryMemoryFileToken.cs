namespace Oldsu.BeatmapSubmission.Memory;

public interface ITemporaryMemoryFileToken: IAsyncDisposable
{
    Stream CreateReadOnlyStream();
}