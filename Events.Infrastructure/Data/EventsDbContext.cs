using Events.Core.Entities;
using Events.Infrastructure.Data.EntityTypeConfigurations;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Events.Infrastructure.Data
{
    public class EventsDbContext : IdentityDbContext<User>
    {
        public EventsDbContext(DbContextOptions<EventsDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new EventConfiguration());
            modelBuilder.ApplyConfiguration(new OfficeConfiguration());
            modelBuilder.ApplyConfiguration(new RoleConfiguration());
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<FileMetaData> Files { get; set; }
        public DbSet<Office> Offices { get; set; }
    }
}
