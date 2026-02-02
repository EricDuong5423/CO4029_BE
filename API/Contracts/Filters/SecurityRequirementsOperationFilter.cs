using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

public class SecurityRequirementsOperationFilter : IOperationFilter
{
    // 1. Bỏ dấu "/" ở cuối các endpoint để so sánh dễ hơn
    private readonly List<string> listPrefixesNeedProtect = new List<string> {
        "users/me",
        "users/update",
        "users/delete-user",
        "users/update-customer",
        "chat/create-history",
        "chat/get-all-history",
        "buildings" // Chỉ cần để "buildings", nó sẽ khớp cả "buildings" và "buildings/{id}"
    };

    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        // Lấy đường dẫn endpoint hiện tại (VD: "buildings/{buildingId}")
        string currentEndpoint = context.ApiDescription.RelativePath;

        // 2. Logic mới: Kiểm tra xem endpoint hiện tại có "BẮT ĐẦU" bằng các prefix kia không
        // StringComparison.OrdinalIgnoreCase để không phân biệt hoa thường
        if (currentEndpoint != null && listPrefixesNeedProtect.Any(prefix => currentEndpoint.StartsWith(prefix, StringComparison.OrdinalIgnoreCase)))
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
                                Id = "Bearer" // Phải khớp với tên bạn định nghĩa trong AddSecurityDefinition ở Program.cs
                            }
                        },
                        Array.Empty<string>()
                    }
                }
            };
        }
    }
}