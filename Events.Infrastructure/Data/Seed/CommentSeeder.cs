using Events.Core.Entities;

namespace Events.Infrastructure.Data.Seed
{
    public class CommentSeeder : ISeed
    {
        public int Order => 3;

        public void Run(EventsDbContext context)
        {
            if (context.Comments.Any()) return;

            Event event1 = context.Events.First();

            context.Comments.Add(new Comment("Why", "Why are we holding this seemingly pointless Event?", event1));
            context.Comments.Add(new Comment("Why?", "Because it is very important!", event1));

            context.SaveChanges();
        }
    }
}
