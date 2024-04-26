using Oldsu.BeatmapSubmission.Exceptions;
using Oldsu.BeatmapSubmission.Extensions;
using OsuParsers.Beatmaps;
using OsuParsers.Decoders;

namespace Oldsu.BeatmapSubmission;

public sealed class BeatmapDecoder
{
    private static async Task<Beatmap> InternalDecodeAsync(Stream stream, CancellationToken cancellationToken = default)
    {
        var lines = await new StreamReader(stream).ReadAllLinesAsync(cancellationToken);
        
        try
        {
            return OsuParsers.Decoders.BeatmapDecoder.Decode(lines);
        }
        catch (Exception exception)
        {
            throw new InvalidOsuFileFormatException(exception);
        }
    }

    public static async Task<List<Beatmap>> DecodeManyAsync(
        IEnumerable<Stream> streams,
        CancellationToken cancellationToken = default,
        bool leaveOpen = false)
    {
        List<Beatmap> beatmaps = [];
        List<Stream> cachedStreams = [];
        
        foreach (var stream in streams)
        {
            var beatmap = await InternalDecodeAsync(stream, cancellationToken);

            beatmaps.Add(beatmap);
            cachedStreams.Add(stream);
        }

        if (!leaveOpen)
        {
            foreach (var stream in cachedStreams)
                stream.Close();
        }

        return beatmaps;
    }
    
    public async Task<Beatmap> DecodeAsync(
        Stream stream, 
        bool leaveOpen = false, 
        CancellationToken cancellationToken = default)
    {
        var beatmap = await InternalDecodeAsync(stream, cancellationToken);
        
        if (!leaveOpen)
            stream.Close();
        
        return beatmap;
    }
}