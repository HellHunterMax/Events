using Microsoft.OpenApi.Models;

namespace Events.Web.Extentions
{
    public static class SwaggerExtentions
    {
        public static void ConfigureSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(config =>
            {
                config.CustomSchemaIds(type => type.ToString());
                //appcontext base directory is where the app entry point assembly is (bin folder)

                //add authorization option to Swagger UI
                config.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header **_WITHOUT_** 'Bearer'. Example: '12345abcdef')",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Scheme = "Bearer"
                });

                //make sure swagger uses authorization token in requests
                config.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        Array.Empty<string>()
                    }
                });
            });
        }
    }
}
