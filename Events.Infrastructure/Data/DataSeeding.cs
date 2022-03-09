using Events.Infrastructure.Data.Seed;

namespace Events.Infrastructure.Data
{
    public static class DataSeeding
    {
        public static void Initialize(EventsDbContext context)
        {
            var seedClasses = AppDomain.CurrentDomain.GetAssemblies().SelectMany(x => x.GetTypes())
            .Where(x => typeof(ISeed).IsAssignableFrom(x) && !x.IsInterface && !x.IsAbstract)
            .Select(x => (ISeed)Activator.CreateInstance(x)!).OrderBy(x => x.Order).ToList();

            foreach (var seedClass in seedClasses)
            {
                seedClass.Run(context);
            }
        }
    }
}
