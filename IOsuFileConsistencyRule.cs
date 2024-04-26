using OsuParsers.Beatmaps;

namespace Oldsu.BeatmapSubmission;

public interface IOsuFileConsistencyRule
{
    bool Check(IReadOnlyList<Beatmap> beatmaps, Creator creator, UnpackedOsz unpackedOsz);
    
    string RuleIdentifier { get; }
}