using Oldsu.BeatmapSubmission.Exceptions;
using Oldsu.BeatmapSubmission.Rules;
using OsuParsers.Beatmaps;

namespace Oldsu.BeatmapSubmission;

public abstract class OsuFileConsistencyChecker
{
    protected abstract IReadOnlyList<IOsuFileConsistencyRule> Rules { get; }
    
    public void EnsureConsistentAsync(IReadOnlyList<Beatmap> beatmaps, Creator creator, UnpackedOsz unpackedOsz)
    {
        foreach (var rule in Rules)
        {
            if (!rule.Check(beatmaps, creator, unpackedOsz))
                throw new OsuFileConsistencyRuleViolationException(rule);
        }
    }
}

public class DefaultOsuFileConsistencyChecker: OsuFileConsistencyChecker
{
    protected override IReadOnlyList<IOsuFileConsistencyRule> Rules { get; } =
    [
        MetadataMatchesConsistencyRule.Instance,
        UsernameMatchesConsistencyRule.Instance,
        DistinctDifficultyNameConsistencyRule.Instance,
        AudioFileExistsConsistencyRule.Instance
    ];
}