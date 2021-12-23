using BaseCore.Commands;
using BaseCore.Data;
using Core.Adapters.Queries;
using Core.Adapters.SqlServer;
using Core.Application.Importacao.Commands.Inputs;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Application.Importacao.Commands
{
    public class ImportacaoCommandHandler : ICommandHandler<ImportacaoCommand>
    {
        private readonly IUnitOfWork UnitOfWork;

        public ImportacaoCommandHandler
        (
            IUnitOfWork unitOfWork
        )
        {
            UnitOfWork = unitOfWork;
        }

        public async Task<IResult> ExecuteAsync(ImportacaoCommand command)
        {
            command.Validate();

            if (!command.Invalid)
            {
                
                //var mensagens = await ImportacaoQuery.ListarMensagensPorEnumeradores(validacaoResult.Erros);

                try
                {
                    

                    UnitOfWork.Commit();
                }
                catch (Exception e)
                {
                    UnitOfWork.Rollback();
                }

                return await Task.FromResult(Result.Ok());
            }
            return await Task.FromResult(Result.Fail(command.Notifications));
            
        }
    }
}
