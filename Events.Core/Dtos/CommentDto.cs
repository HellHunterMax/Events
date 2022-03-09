using Events.Core.Shared.Enums;

namespace Events.Core.Dtos
{
    public class CommentDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public Guid EventId { get; set; }

        public Status Status { get; set; } = Status.Active;
    }
}
