using BaseCore.Commands;
using BaseCore.Helps;
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
            services.AddOptions<SecretKeysSettings>()
                .Bind(configuration.GetSection(nameof(SecretKeysSettings)));

            services.AddTransient<ISecretsKeyHolder, SecretsKeyHolder>();

            services.AddScoped<DependencyResolver, DependencyResolver>();
            services.AddScoped<ICommandDispatcher, CommandDispatcher>();
            services.AddScoped<IQueryProcessor, QueryProcessor>();

            return services;
        }
    }
}
