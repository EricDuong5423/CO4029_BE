using AgenticAR.Infrastructure.Repository;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Supabase;

namespace AgenticAR.Infrastructure
{
    public static class DependencyInjection
    {
        public static string supabaseApiKey;
        public static string supabaseUrl;
        public static IServiceCollection AddInfrastructureServices(
            this IServiceCollection services,
            IConfiguration config)
        {
            supabaseApiKey = config["supabaseApiKey"];
            supabaseUrl = config["supabaseUrl"];
            services.AddSingleton<Supabase.Client>(_ =>
                new Supabase.Client(
                    supabaseUrl,
                    supabaseApiKey,
                    new SupabaseOptions
                    {
                        AutoRefreshToken = true,
                        AutoConnectRealtime = true
                    }
                )
            );
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IOtpCodeRepository, OtpCodeRepository>();
            services.AddScoped<IHistoryRepository, HistoryRepository>();
            services.AddScoped<IBuildingRepository, BuildingRepository>();
            return services;
        }
    }
}