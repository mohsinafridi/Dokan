using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace Order.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationService(this IServiceCollection services, IConfiguration configuration)
        {
            return services;
        }
    }
}
