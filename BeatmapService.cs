using System.Security.Cryptography.X509Certificates;
using Oldsu.BeatmapSubmission.Exceptions;
using Oldsu.BeatmapSubmission.Repositories;
using OsuParsers.Decoders;

namespace Oldsu.BeatmapSubmission;

public sealed class BeatmapService(
    OszUnpacker oszUnpacker,
    OsuFileConsistencyChecker consistencyChecker,
    IBeatmapsetMetadataRepository beatmapsetMetadataRepository)
{
    public async Task SubmitBeatmapAsync(Stream oszStream, Creator creator, CancellationToken cancellationToken = default)
    {
        if (creator.AvailableUploads <= 0)
            throw new UserUploadsExceededException();
        
        await using var unpackedOsz = await oszUnpacker.UnpackAsync(oszStream, leaveOpen: true, cancellationToken);
        
        var beatmaps = await BeatmapDecoder.DecodeManyAsync(
            unpackedOsz.OsuFiles.Select(osuFile => osuFile.CreateReadOnlyStream()), cancellationToken);

        consistencyChecker.EnsureConsistentAsync(beatmaps, creator, unpackedOsz);
        
        // TODO move to permanent location and register the newly uploaded beatmap into the database
    }
}