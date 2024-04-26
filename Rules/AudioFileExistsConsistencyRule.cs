using OsuParsers.Beatmaps;

namespace Oldsu.BeatmapSubmission.Rules;

public class AudioFileExistsConsistencyRule: IOsuFileConsistencyRule
{
    public static readonly AudioFileExistsConsistencyRule Instance = new();
    
    private AudioFileExistsConsistencyRule() {}
    
    public bool Check(IReadOnlyList<Beatmap> beatmaps, Creator creator, UnpackedOsz unpackedOsz) =>
        beatmaps.All(b => unpackedOsz.Files.ContainsKey(b.GeneralSection.AudioFilename));

    public string RuleIdentifier => "AUDIO_FILE_NOT_EXISTS";
}