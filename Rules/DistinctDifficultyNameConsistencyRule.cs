using OsuParsers.Beatmaps;

namespace Oldsu.BeatmapSubmission.Rules;

public class DistinctDifficultyNameConsistencyRule: IOsuFileConsistencyRule
{
    public static readonly DistinctDifficultyNameConsistencyRule Instance = new();
    
    private DistinctDifficultyNameConsistencyRule() {}
    
    public bool Check(IReadOnlyList<Beatmap> beatmaps, Creator creator, UnpackedOsz unpackedOsz)
    {
        var diffNames = beatmaps.Select(b => b.MetadataSection.Version).ToArray();
        return diffNames.Distinct().Count() == diffNames.Length;
    }

    public string RuleIdentifier => "DUPLICATED_DIFFICULTY_NAME";
}