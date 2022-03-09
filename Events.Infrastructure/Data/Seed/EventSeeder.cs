using Events.Core.Entities;
using Events.Core.Shared.ValueObjects;

namespace Events.Infrastructure.Data.Seed
{
    public class EventSeeder : ISeed
    {
        public int Order => 2;

        public void Run(EventsDbContext context)
        {
            if (context.Events.Any()) return;

            Office office = context.Offices.First();
            Category category = context.Categories.First();
            var almere = new Location("Street", "1", "Almere", "1313AB", 5.5f, 0.5f);


            Event @event = new(
                    "Standup",
                    "we meet for the sprint standup",
                    new Duration(new DateTime(2000, 1, 1, 0, 15, 0),
                    new DateTime(2000, 1, 1, 0, 15, 0)), office,
                    almere,
                    new List<Category>() { category }
                    );

            context.Events.Add(@event);

            context.SaveChanges();
        }
    }
}
