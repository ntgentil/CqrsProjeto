using BaseCore.Commands;
using Core.Adapters.Queries;
using Core.Application.Importacao.Commands;
using Core.Application.Importacao.Commands.Inputs;
using Core.Application.Importacao.Queries;
using Microsoft.Extensions.DependencyInjection;

namespace Core
{
    public static class DependencyRegister
    {
        public static IServiceCollection RegisterValidacaoCalculoDependecies(this IServiceCollection services)
        {
            services.AddScoped<ICommandHandler<ImportacaoCommand>, ImportacaoCommandHandler>();

            return services;
        }
    }
}
