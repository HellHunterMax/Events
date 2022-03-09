using Azure.Storage.Blobs;
using Events.Core.Options;
using Events.Web.Extentions;
using Events.Web.Options;

namespace Events.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        private readonly IConfiguration Configuration;

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers().AddNewtonsoftJson(x => x.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddEndpointsApiExplorer();
            services.ConfigureSwagger();

            services.ConfigureDatabase(Configuration);

            services.ConfigureRepository();
            services.ConfigureDIServices();
            services.AddSingleton(new BlobServiceClient(Configuration.GetValue<string>("AzureStorageSettings:AzureStorageConnectionString")));

            services.AddAuthentication();
            services.ConfigureIdentity();
            services.ConfigureOptions(Configuration);

            var jwtSettings = Configuration.GetSection(JwtSettings.Name).Get<JwtSettings>();
            services.ConfigureJWT(jwtSettings);

        }

        public void Configure(IApplicationBuilder app, IHostEnvironment env)
        {
            // Configure the HTTP request pipeline.
            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints => endpoints.MapControllers());
        }
    }
}
