namespace Oldsu.BeatmapSubmission.Exceptions;

public sealed class OsuFileConsistencyRuleViolationException(IOsuFileConsistencyRule rule): Exception("The osu file violates the consistency rules")
{
    public IOsuFileConsistencyRule Rule { get; } = rule;
}