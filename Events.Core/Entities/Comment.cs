using Events.Core.Shared.Enums;
using Events.Core.Shared.Extentions;
using System.ComponentModel.DataAnnotations;

namespace Events.Core.Entities
{
    public class Comment : BaseEntity
    {
        private Comment() { }
        public Comment(string title, string text, Event @event, Status status = Status.Active)
        {
            SetTitle(title);
            SetText(text);
            Event = @event.ThrowIfNull(nameof(@event));
            Status = status;
        }

        [MaxLength(255)]
        [MinLength(2)]
        [Required]
        public string Title { get; private set; }
        [Required]
        public string Text { get; private set; }
        [Required]
        public Event Event { get; private set; }
        public DateTime CreationTime { get; set; } = DateTime.UtcNow;
        public Status Status { get; private set; } = Status.Active;

        public void SetTitle(string title)
        {
            title.ThrowIfNullOrWhiteSpace(nameof(title));
            if (title.Length < 2 || title.Length > 255) throw new ArgumentOutOfRangeException(nameof(title));
            Title = title;
        }
        public void SetText(string text)
        {
            Text = text.ThrowIfNullOrWhiteSpace(nameof(text));
        }
        public void SetStatus(Status status)
        {
            Status = status;
        }
        //TODO addUser to Comment
    }
}
