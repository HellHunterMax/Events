using Events.Core.Options;
using Events.Web.Options;

namespace Events.Web.Extentions
{
    public static class OptionsServiceCollectionExtentions
    {
        public static void ConfigureOptions(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<DiskStorageSettings>(configuration.GetSection(nameof(DiskStorageSettings)));
            services.Configure<AzureStorageSettings>(configuration.GetSection(nameof(AzureStorageSettings)));
            services.Configure<JwtSettings>(configuration.GetSection(nameof(JwtSettings)));
            services.Configure<SendGridSettings>(configuration.GetSection(nameof(SendGridSettings)));
        }
    }
}
