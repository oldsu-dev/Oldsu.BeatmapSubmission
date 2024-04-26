namespace Oldsu.BeatmapSubmission.Repositories;

public interface ICreatorRepository
{ 
    Task<Creator?> FindUserAsync(CreatorId id, CancellationToken cancellationToken = default);
}