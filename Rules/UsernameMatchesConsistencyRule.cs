using OsuParsers.Beatmaps;

namespace Oldsu.BeatmapSubmission.Rules;

public class UsernameMatchesConsistencyRule: IOsuFileConsistencyRule
{
    public static readonly UsernameMatchesConsistencyRule Instance = new();
    
    private UsernameMatchesConsistencyRule() {}
    
    public bool Check(IReadOnlyList<Beatmap> beatmaps, Creator creator, UnpackedOsz unpackedOsz) =>
        beatmaps.All(b => b.MetadataSection.Creator == creator.Name.Value);

    public string RuleIdentifier => "USERNAME_MISMATCH";
}