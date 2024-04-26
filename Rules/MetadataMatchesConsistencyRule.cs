using OsuParsers.Beatmaps;

namespace Oldsu.BeatmapSubmission.Rules;

public class MetadataMatchesConsistencyRule: IOsuFileConsistencyRule
{
    public static readonly MetadataMatchesConsistencyRule Instance = new();
    
    private MetadataMatchesConsistencyRule() {}
    
    public bool Check(IReadOnlyList<Beatmap> beatmaps, Creator creator, UnpackedOsz unpackedOsz)
    {
        Beatmap? beatmap = beatmaps.FirstOrDefault();
        if (beatmap == null)
            return true;

        return beatmaps.Skip(1).All(b =>
            b.MetadataSection.Creator == beatmap.MetadataSection.Creator &&
            b.MetadataSection.Artist == beatmap.MetadataSection.Artist &&
            b.MetadataSection.Title == beatmap.MetadataSection.Title);
    }
    
    public string RuleIdentifier => "METADATA_MISMATCH";
}