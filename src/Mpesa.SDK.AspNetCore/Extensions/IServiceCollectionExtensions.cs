using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Mpesa.SDK.AspNetCore
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection AddMpesaSdk(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<LipaNaMpesaOptions>(configuration.GetSection($"Mpesa:{LipaNaMpesaOptions.Name}"));
            services.Configure<C2BOptions>(configuration.GetSection($"Mpesa:{C2BOptions.Name}"));
            services.Configure<B2COptions>(configuration.GetSection($"Mpesa:{B2COptions.Name}"));

            services.AddScoped<ILipaNaMpesa, LipaNaMpesa>();
            services.AddScoped<IC2B, C2B>();
            services.AddScoped<IB2C, B2C>();

            return services;
        }
    }
}
