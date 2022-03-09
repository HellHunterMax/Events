using Events.Core.Shared.Enums;

namespace Events.Core.Dtos
{
    public class CategoryDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Status Status { get; set; }
    }
}
