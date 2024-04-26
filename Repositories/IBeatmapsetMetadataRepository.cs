namespace Oldsu.BeatmapSubmission.Repositories;

public interface IBeatmapsetMetadataRepository
{ 
    Task SaveAsync(BeatmapsetMetadata beatmapsetMetadata, CancellationToken cancellationToken = default);
}