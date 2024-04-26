namespace Oldsu.BeatmapSubmission.Extensions;

public static class StreamReaderExtensions
{
    public static async Task<List<string>> ReadAllLinesAsync(this StreamReader reader, CancellationToken cancellationToken = default)
    {
        List<string> lines = [];

        while (await reader.ReadLineAsync() is { } current)
            lines.Add(current);

        return lines;
    }
}