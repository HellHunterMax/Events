using Events.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Events.Infrastructure.Data.EntityTypeConfigurations
{
    public class EventConfiguration : IEntityTypeConfiguration<Event>
    {
        public void Configure(EntityTypeBuilder<Event> builder)
        {
            builder.OwnsOne(e => e.Duration);
            builder.OwnsOne(e => e.Location);
            builder.OwnsMany(e => e.Attendees);
        }
    }
}
