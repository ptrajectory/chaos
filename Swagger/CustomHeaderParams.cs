
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace chaos.Swagger;

public class AddCustomHeaderParameter : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        var isAuthEndpoint = context.ApiDescription.RelativePath?.Equals("api/organizations/{organization_id}/apps/{app_id}/authorize", StringComparison.OrdinalIgnoreCase) ?? false;
        
        if(isAuthEndpoint)
        {
            if(operation.Parameters == null)
            {
                operation.Parameters = new System.Collections.Generic.List<OpenApiParameter>();
            }
            
            operation.Parameters.Add(new OpenApiParameter{
                Name = "x-app-id",
                In = ParameterLocation.Header,
                Required = false,
                Schema = new OpenApiSchema { Type = "string" },
                Description = "The App ID"
            });

            operation.Parameters.Add(new OpenApiParameter{
                Name = "x-app-secret",
                In = ParameterLocation.Header,
                Required = false,
                Schema = new OpenApiSchema { Type = "string" },
                Description = "The App Secret"
            });
        }
    }
}