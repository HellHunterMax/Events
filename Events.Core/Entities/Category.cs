using Events.Core.Shared.Enums;
using Events.Core.Shared.Extentions;
using System.ComponentModel.DataAnnotations;

namespace Events.Core.Entities
{
    public class Category : BaseEntity
    {
        private Category() { }
        public Category(string name, Status status = Status.Active)
        {
            SetName(name);
            SetStatus(status);
        }
        [MaxLength(255)]
        [Required]
        public string Name { get; private set; }
        public Status Status { get; private set; } = Status.Active;

        public void SetName(string name)
        {
            if (name.Length > 255)
            {
                throw new ArgumentException($"{nameof(name)} is too long can only be 255 but was: {name.Length}.");
            }
            Name = name.ThrowIfNullOrWhiteSpace(nameof(name));
        }

        public void SetStatus(Status status)
        {
            Status = status;
        }

        public override string ToString()
        {
            return $"Name: {Name}, Id: {Id}";
        }
    }
}
