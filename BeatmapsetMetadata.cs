using OsuParsers.Beatmaps;

namespace Oldsu.BeatmapSubmission;

public record BeatmapsetTitle(string Value) {}
public record BeatmapsetArtist(string Value) {}

public class BeatmapsetMetadata(BeatmapsetArtist artist, BeatmapsetTitle title, Creator creator)
{
    public BeatmapsetArtist Artist { get; } = artist;
    public BeatmapsetTitle Title { get; } = title;
    public Creator Creator { get; } = creator;
}