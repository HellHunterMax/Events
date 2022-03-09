using Events.Core.Shared.Enums;
using Events.Core.Shared.Extentions;

namespace Events.Core.Entities
{
    public class FileMetaData : BaseEntity
    {
        private FileMetaData() { }
        public FileMetaData(string name, string description, string location, Status status, long length, string contentType)
        {
            SetName(name);
            SetDescription(description);
            SetLocation(location);
            SetStatus(status);
            SetLength(length);
            SetContentType(contentType);
        }

        public string Name { get; private set; }
        public string Description { get; private set; }
        public string Location { get; private set; }
        public Status Status { get; private set; }
        public long Length { get; private set; }
        public string ContentType { get; private set; }


        public void SetName(string name)
        {
            name.ThrowIfNullOrWhiteSpace(nameof(name));
            Name = name;
        }

        public void SetDescription(string description)
        {
            description.ThrowIfNullOrWhiteSpace(nameof(description));
            Description = description;
        }

        public void SetLocation(string location)
        {
            location.ThrowIfNullOrWhiteSpace(nameof(location));
            Location = location;
        }

        public void SetStatus(Status status)
        {
            Status = status;
        }

        public void SetLength(long length)
        {
            if (length <= 0) throw new ArgumentOutOfRangeException(nameof(length));
            Length = length;
        }

        public void SetContentType(string contentType)
        {
            contentType.ThrowIfNullOrWhiteSpace(nameof(contentType));
            ContentType = contentType;
        }
    }
}
