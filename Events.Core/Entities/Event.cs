using Events.Core.Shared.Enums;
using Events.Core.Shared.Extentions;
using Events.Core.Shared.ValueObjects;
using System.ComponentModel.DataAnnotations;

namespace Events.Core.Entities
{
    public class Event : BaseEntity
    {
        private Event() { }
        public Event(
            string name,
            string discription,
            Duration duration,
            Office office,
            Location location,
            List<Category>? categories = null,
            List<Email> attendees = null,
            List<FileMetaData>? files = null,
            Status status = Status.Active
            )
        {
            SetName(name);
            SetDiscription(discription);
            SetOffice(office);
            SetLocation(location);
            SetDuration(duration);
            SetStatus(status);
            SetCategories(categories);
            SetAttendees(attendees);

            if (files == null) files = new();
            _files = files;
        }

        [MaxLength(255)]
        [Required]
        public string Name { get; private set; }
        public string Discription { get; private set; }
        public Duration Duration { get; private set; }
        public Office Office { get; private set; }
        public Location Location { get; private set; }
        public int NumberOfAnttendees => Attendees.Count;
        private List<Category> _categories = new();
        public IReadOnlyCollection<Category> Categories => _categories.AsReadOnly();
        public Status Status { get; private set; } = Status.Active;
        private List<Email> _attendees = new();
        public IReadOnlyCollection<Email> Attendees => _attendees.AsReadOnly();
        private List<Comment> _comments = new();
        public IReadOnlyCollection<Comment> Comments => _comments.AsReadOnly();
        private List<FileMetaData> _files = new();
        public IReadOnlyCollection<FileMetaData> Files => _files.AsReadOnly();

        public void SetName(string name)
        {
            Name = name.ThrowIfNullOrWhiteSpace(nameof(name));
        }
        public void SetDiscription(string discription)
        {
            Discription = discription.ThrowIfNullOrWhiteSpace(nameof(discription));
        }
        public void SetOffice(Office office)
        {
            Office = office.ThrowIfNull(nameof(office));
        }
        public void SetLocation(Location location)
        {
            Location = location.ThrowIfNull(nameof(location));
        }
        public void SetDuration(Duration duration)
        {
            Duration = duration.ThrowIfNull(nameof(duration));
        }
        public void SetCategories(List<Category> categories)
        {
            if (categories == null) categories = new();
            _categories = categories;

        }
        public void SetStatus(Status status)
        {
            Status = status;
        }
        internal void SetAttendees(List<Email> attendees)
        {
            if (attendees is null) attendees = new();
            _attendees = attendees;
        }

        public void AddFile(FileMetaData file)
        {
            _files.Add(file);
        }

        public override string ToString()
        {
            return $"{Name}, {Discription}, Start Date = {Duration.Start}, End Date = {Duration.End}, Office: {Office}, Location: {Location.City}.";
        }

    }
}
