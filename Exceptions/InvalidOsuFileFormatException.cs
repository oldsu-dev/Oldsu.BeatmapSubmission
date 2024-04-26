namespace Oldsu.BeatmapSubmission.Exceptions;

public sealed class InvalidOsuFileFormatException(Exception innerException)
    : Exception("The osu file has an invalid format", innerException)
{
    
}