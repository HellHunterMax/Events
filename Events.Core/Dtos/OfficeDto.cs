using Events.Core.Shared.Enums;
using Events.Core.Shared.ValueObjects;

namespace Events.Core.Dtos
{
    public class OfficeDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Discription { get; set; }
        public Status Status { get; set; } = Status.Active;
        public Location Location { get; set; }
        public Email Email { get; set; }
        public PhoneNumber PhoneNumber { get; set; }
    }
}
