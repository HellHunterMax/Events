using Events.Core.Entities;
using Events.Core.Shared.ValueObjects;

namespace Events.Infrastructure.Data.Seed
{
    public class OfficeSeeder : ISeed
    {
        public int Order => 1;

        public void Run(EventsDbContext context)
        {
            if (context.Offices.Any()) return;

            var almere = new Location("Street", "1", "Almere", "1313AB", 52.35f, 5.26f);
            var amsterdam = new Location("Street", "1", "Amsterdam", "1313AB", 52.35f, 5.26f);
            var rotterdam = new Location("Street", "1", "Rotterdam", "1313AB", 52.35f, 5.26f);

            context.Offices.Add(new Office("Almere", "Main HQ in Almere", almere, new Email("almere@mail.nl"), new PhoneNumber("0611111111")));
            context.Offices.Add(new Office("Amsterdam", "Main HQ in Amsterdam", amsterdam, new Email("amsterdam@mail.nl"), new PhoneNumber("0611111112")));
            context.Offices.Add(new Office("Rotterdam", "Main HQ in Rotterdam", rotterdam, new Email("rotterdam@mail.nl"), new PhoneNumber("0611111113")));

            context.SaveChanges();
        }
    }
}
