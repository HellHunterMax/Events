namespace Events.Infrastructure.Data.Seed
{
    public interface ISeed
    {
        public int Order { get; }
        void Run(EventsDbContext context);
    }
}
