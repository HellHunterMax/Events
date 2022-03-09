using Events.Core.Emails;
using Events.Core.Interfaces;
using Events.Core.Services;
using Events.Infrastructure.Repositories;
using Events.Infrastructure.Services;
using Events.Web.Services;

namespace Events.Web.Extentions
{
    public static class DIServiceExtentions
    {
        public static void ConfigureRepository(this IServiceCollection services)
        {
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        }

        public static void ConfigureDIServices(this IServiceCollection services)
        {
            services.AddScoped<IFileStorageService, AzureBlobStorageService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IOfficeService, OfficeService>();
            services.AddScoped<IEventService, EventService>();
            services.AddScoped<ICommentService, CommentService>();
            services.AddScoped<IAuthenticationManager, AuthenticationManager>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IEmailSenderService, SendGridService>();
            services.AddScoped<IEmailFactory, EmailFactory>();
            services.AddScoped<IEmailService, EmailService>();
        }
    }
}
