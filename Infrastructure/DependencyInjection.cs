using AgenticAR.Infrastructure.Repository;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Supabase;

namespace AgenticAR.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureServices(
            this IServiceCollection services,
            IConfiguration config)
        {
            services.AddSingleton<Supabase.Client>(_ =>
                new Supabase.Client(
                    config["SupabaseUrl"],
                    config["SupabaseApiKey"],
                    new SupabaseOptions
                    {
                        AutoRefreshToken = true,
                        AutoConnectRealtime = true
                    }
                )
            );
            services.AddScoped<IUserRepository, UserRepository>();
            return services;
        }
    }
}