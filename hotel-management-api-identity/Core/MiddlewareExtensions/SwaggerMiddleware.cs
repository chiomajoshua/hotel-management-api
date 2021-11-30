using Microsoft.OpenApi.Models;
using System.Reflection;

namespace hotel_management_api_identity.Core.MiddlewareExtensions
{
    public class SwaggerOptions
    {
        public string Title { get; set; }
        public string Version { get; set; }
    }

    public static class Extensions
    {
        public static IServiceCollection AddSwaggerService(this IServiceCollection services)
        {
            _ = services.AddSwaggerGen(c =>
              {
                  c.SwaggerDoc("v1", new OpenApiInfo { Title = "House 4 MS API V1", Version = $"1.0" });
#pragma warning disable CS8602 // Dereference of a possibly null reference.
                  //c.IncludeXmlComments(Path.ChangeExtension(Assembly.GetEntryAssembly().Location, "xml"));
#pragma warning restore CS8602 // Dereference of a possibly null reference.
                  c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                  {
                      Description = "",
                      Name = "Authorization",
                      In = ParameterLocation.Header,
                      Type = SecuritySchemeType.ApiKey
                  });
                  c.CustomSchemaIds(x => x.FullName);
                  c.AddSecurityRequirement(new OpenApiSecurityRequirement {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        System.Array.Empty<string>()
                    }
                  });

              });
            return services;
        }

        public static IApplicationBuilder UseSwaggerService(this IApplicationBuilder app, IHostEnvironment environment)
        {
            if (!environment.IsProduction())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "House 4 MS API V1");
                });
            }
            return app;
        }
    }
}