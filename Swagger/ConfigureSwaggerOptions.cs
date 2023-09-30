
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace chaos.Swagger;

public class ConfigureSwaggerOptions : IConfigureOptions<SwaggerGenOptions>
{
    void IConfigureOptions<SwaggerGenOptions>.Configure(SwaggerGenOptions options)
    {
        options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme{
            In = ParameterLocation.Header,
            Description = "Provide a valid token",
            Name = "Authorization", 
            Type = SecuritySchemeType.Http,
            BearerFormat = "JWT",
            Scheme = "Bearer"
        });

        options.AddSecurityRequirement(new OpenApiSecurityRequirement{

            {
                new OpenApiSecurityScheme 
                {
                    Reference = new OpenApiReference{
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer",
                    }
                },
                
                Array.Empty<string>()
            }

        });


    }
}