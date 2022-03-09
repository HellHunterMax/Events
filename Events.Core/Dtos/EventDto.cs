using Events.Core.Shared.Enums;
using Events.Core.Shared.ValueObjects;

namespace Events.Core.Dtos
{
    public class EventDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Discription { get; set; }
        public Duration Duration { get; set; }
        public Guid OfficeId { get; set; }
        public Location Location { get; set; }
        public List<Guid> CategoryIds { get; set; }
        public Status Status { get; set; } = Status.Active;
        public List<Email> Attendees { get; set; }
    }
}
