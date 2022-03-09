using Events.Core.Shared.Enums;
using Events.Core.Shared.Extentions;
using Events.Core.Shared.ValueObjects;
using System.ComponentModel.DataAnnotations;

namespace Events.Core.Entities
{
    public class Office : BaseEntity
    {
        private Office() { }
        public Office(string name, string discription, Location location, Email email, PhoneNumber phoneNumber, Status status = Status.Active)
        {
            SetName(name);
            SetDescription(discription);
            SetLocation(location);
            SetEmail(email);
            SetPhoneNumber(phoneNumber);
            SetStatus(status);
        }

        [MaxLength(255)]
        [Required]
        public string Name { get; private set; }
        public string Discription { get; private set; }
        public Status Status { get; private set; } = Status.Active;
        public Location Location { get; private set; }
        public Email Email { get; private set; }
        public PhoneNumber PhoneNumber { get; private set; }

        public void SetName(string name)
        {
            name.ThrowIfNullOrWhiteSpace(nameof(name));
            if (name.Length > 255) throw new ArgumentException($"Name is too long. Can only be 255 chars but was {name.Length}");

            Name = name;
        }
        public void SetDescription(string discription)
        {
            Discription = discription.ThrowIfNullOrWhiteSpace(nameof(discription));
        }
        public void SetLocation(Location location)
        {
            Location = location.ThrowIfNull(nameof(location));
        }
        public void SetEmail(Email email)
        {
            Email = email.ThrowIfNull(nameof(email));
        }
        public void SetPhoneNumber(PhoneNumber phoneNumber)
        {
            PhoneNumber = phoneNumber.ThrowIfNull(nameof(phoneNumber));
        }

        public void SetStatus(Status status)
        {
            Status = status;
        }
    }
}
