using Events.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Events.Web.Extentions
{
    public static class DatabaseServiceCollectionExtention
    {
        public static void ConfigureDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            services
                .AddDbContext<EventsDbContext>(options =>
                options
                .UseSqlServer(configuration.GetConnectionString("DefaultConnectionString")!, b => b.MigrationsAssembly("Events.Infrastructure")));
        }
    }
}
