using AgenticAR.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace AgenticAR.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationServices(
            this IServiceCollection services)
        {
            services.AddScoped<AuthService>();
            services.AddScoped<UserService>();
            return services;
        }
    }
}