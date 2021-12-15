using BaseCore.Commands;
using BaseCore.Queries;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace BaseCore
{
    public static class DependencyRegister
    {
        public static IServiceCollection RegisterCoreDependencies(
            this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<DependencyResolver, DependencyResolver>();
            services.AddScoped<ICommandDispatcher, CommandDispatcher>();
            services.AddScoped<IQueryProcessor, QueryProcessor>();

            return services;
        }
    }
}
