using BaseCore.Commands;
using Core.Application.Importacao.Commands;
using Core.Application.Importacao.Commands.Inputs;
using Microsoft.Extensions.DependencyInjection;

namespace Core
{
    public static class DependencyRegister
    {
        public static IServiceCollection RegisterValidacaoCalculoDependecies(this IServiceCollection services)
        {
            services.AddScoped<ICommandHandler<ImportacaoCommand>, ImportacaoCommandHandler>();
            //services.AddScoped<IValidacaoCalculoQuery, ValidacaoCalculoQuery>();

            return services;
        }
    }
}
