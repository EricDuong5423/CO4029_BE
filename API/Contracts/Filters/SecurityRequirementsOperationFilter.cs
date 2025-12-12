using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

public class SecurityRequirementsOperationFilter : IOperationFilter
{
    private List<string> listEndPointNeedProtect = new List<string> {
        "users/me",
        "users/update",
        "users/request-password-change",
        "users/change-password",
        "users/delete-user"
    };
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        string endpoint = context.ApiDescription.RelativePath;
        
        if (listEndPointNeedProtect.Contains(endpoint))
        {
            operation.Security = new List<OpenApiSecurityRequirement>
            {
                new OpenApiSecurityRequirement
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
                }
            };
        }
    }
}