using Events.Core.Entities;

namespace Events.Infrastructure.Data.Seed
{
    public class CategorySeeder : ISeed
    {
        public int Order => 0;

        public void Run(EventsDbContext context)
        {
            if (context.Categories.Any()) return;

            context.Categories.Add(new Category("Live"));
            context.Categories.Add(new Category("Music"));
            context.Categories.Add(new Category("Disco"));
            context.Categories.Add(new Category("Meeting"));
            context.Categories.Add(new Category("Office"));
            context.Categories.Add(new Category("Training"));

            context.SaveChanges();
        }
    }
}
