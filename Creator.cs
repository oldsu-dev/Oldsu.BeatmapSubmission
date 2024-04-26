namespace Oldsu.BeatmapSubmission;

public record CreatorName(string Value) {}
public record struct CreatorId(int Value) {}

public sealed class Creator
{
    public const int InitialAvailableUploads = 5;
    public CreatorName Name { get; private set; } = null!;
    public CreatorId Id { get; private set; }
    public int AvailableUploads { get; private set; }
    public bool Restricted { get; private set; }
    
    public bool CanUpload => AvailableUploads > 0 && !Restricted;
    
    public class Builder(CreatorName creatorName, CreatorId id)
    {
        private int _availableUploads = InitialAvailableUploads;
        private bool _restricted;

        public Builder WithRestricted()
        {
            _restricted = true;
            return this;
        }

        public Builder WithNotRestricted()
        {
            _restricted = false;
            return this;
        }

        public Builder WithAvailableUploads(int availableUploads)
        {
            if (availableUploads < 0)
                throw new ArgumentException($"value must be greater than or equal to 0", nameof(availableUploads));

            _availableUploads = availableUploads;
            return this;
        }

        public Creator Build()
        {
            return new Creator
            {
                AvailableUploads = _availableUploads,
                Id = id,
                Restricted = _restricted,
                Name = creatorName,
            };
        }
    }
}
