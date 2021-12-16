using BaseCore.Queries;
using Core.Adapters.SqlServer;
using Core.Application.Importacao.Queries;
using Core.Application.Importacao.Queries.Inputs;
using Core.Application.Importacao.Queries.Results;
using Microsoft.Extensions.DependencyInjection;

namespace SqlServer
{
    public static class DependencyRegister
    {


        public static IServiceCollection RegisterDomainDependencies(this IServiceCollection services)
        {
            services.AddScoped<DomainDataContext, DomainDataContext>();
            services.AddScoped<IUnitOfWork, DomainUnitOfWork>();

            services
                .RegisterClienteDependecies();

            return services;
        }

        public static IServiceCollection RegisterSqlServerDependencies(this IServiceCollection services, Microsoft.Extensions.Configuration.IConfiguration configuration)
        {
            services.AddScoped<DomainDataContext, DomainDataContext>();
            services.AddScoped<IUnitOfWork, DomainUnitOfWork>();
            services.AddTransient<ISqlServerStoreHolder, SqlServerStoreHolder>();
            

            return services;
        }

        private static IServiceCollection RegisterClienteDependecies(this IServiceCollection services)
        {
            services.AddScoped<ImportacaoQuery, ImportacaoQuery>();
            services.AddScoped<IQueryHandler<ProdutoIdInput, ProdutoResult>, ImportacaoQueryHandler>();

            return services;
        }
    }
}
