using System.IO.Compression;
using Oldsu.BeatmapSubmission.Exceptions;
using Oldsu.BeatmapSubmission.Memory;
using OsuParsers.Beatmaps;
using OsuParsers.Decoders;

namespace Oldsu.BeatmapSubmission;

public sealed class UnpackedOsz(ITemporaryMemoryDirectoryToken directoryToken): IAsyncDisposable
{
    public IReadOnlyList<ITemporaryMemoryDirectoryItem> OsuFiles { get; } =
        directoryToken.Items.Where(f => f.Name.EndsWith(".osu")).ToArray();

    public IReadOnlyDictionary<string, ITemporaryMemoryDirectoryItem> Files { get; } =
        directoryToken.Items.ToDictionary(i => i.Name, i => i);
    
    public ValueTask DisposeAsync() => directoryToken.DisposeAsync();
}

public sealed class OszUnpacker
{
    private readonly ITemporaryMemory _temporaryMemory;
    private readonly IOszPolicy _policy;
    
    public OszUnpacker(ITemporaryMemory temporaryMemory, IOszPolicy policy)
    {
        _temporaryMemory = temporaryMemory;
        _policy = policy;
    }

    private static Task<ZipArchive> OpenOsz(Stream oszStream) =>
        new Task<ZipArchive>(() => new ZipArchive(oszStream));

    private static long ComputeUncompressedOszSize(ZipArchive archive) =>
        archive.Entries.Select(s => s.Length).Sum();
    
    public async Task<UnpackedOsz> UnpackAsync(
        Stream oszStream, 
        bool leaveOpen = false, 
        CancellationToken cancellationToken = default)
    {
        await using var token = await _temporaryMemory.CreateFileFromStreamAsync(oszStream, cancellationToken);
        try
        {
            var archive = await OpenOsz(token.CreateReadOnlyStream());

            if (_policy.IsOszSizeValid(ComputeUncompressedOszSize(archive)))
                throw new TooBigOszException();
            
            if (!archive.Entries.Any(e => e.FullName.EndsWith(".osu")))
                throw new NoOsuFileException();
            
            var directoryItems = archive.Entries.Select(
                entry => new DirectoryItem(entry.Name, entry.Open));
            
            var directoryToken =
                await _temporaryMemory.CreateDirectoryFromItemsAsync(directoryItems, cancellationToken);

            if (!leaveOpen)
                oszStream.Close();
            
            return new UnpackedOsz(directoryToken);
        }
        catch (InvalidDataException exception)
        {
            throw new MalformedOszException();
        }
    }
}