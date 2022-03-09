using Events.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Events.Infrastructure.Data.EntityTypeConfigurations
{
    public class OfficeConfiguration : IEntityTypeConfiguration<Office>
    {
        public void Configure(EntityTypeBuilder<Office> builder)
        {
            builder.OwnsOne(o => o.Email);
            builder.OwnsOne(o => o.PhoneNumber);
            builder.OwnsOne(o => o.Location);
        }
    }
}
