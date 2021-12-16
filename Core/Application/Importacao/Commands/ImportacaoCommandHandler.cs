using BaseCore.Commands;
using BaseCore.Data;
using Core.Adapters.Queries;
using Core.Adapters.SqlServer;
using Core.Application.Importacao.Commands.Inputs;
using Core.Application.Importacao.Commands.Results;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Application.Importacao.Commands
{
    public class ImportacaoCommandHandler : ICommandHandler<ImportacaoCommand>
    {
        private readonly IUnitOfWork UnitOfWork;
        private readonly IImportacaoQuery ImportacaoQuery;

        public ImportacaoCommandHandler
        (
            IUnitOfWork unitOfWork,
            IImportacaoQuery importacaoQuery
        )
        {
            UnitOfWork = unitOfWork;
            ImportacaoQuery = importacaoQuery;
        }

        public async Task<IResult> ExecuteAsync(ImportacaoCommand command)
        {
            command.Validate();

            if (command.Invalid)
            {
                var validacaoResult = ErrosValidacaoResult.Parse(command.Notifications);

                //var mensagens = await ImportacaoQuery.ListarMensagensPorEnumeradores(validacaoResult.Erros);

                try
                {
                    foreach (var erro in validacaoResult.Erros)
                    {
                        //UnitOfWork.HistoricoMensagemRepository.Insert
                        //(
                        //    new HistoricoMensagemEntity
                        //    {
                        //        HistoricoCalculo = HistoricoCalculoEntity.Parse(command),
                        //        IdMensagem = mensagens.Mensagens.FirstOrDefault(x => erro.Id == x.Codigo && erro.Name == x.Mensagem).Id
                        //    }
                        //);
                    }

                    UnitOfWork.Commit();
                }
                catch (Exception e)
                {
                    UnitOfWork.Rollback();
                }

                return await Task.FromResult(Result<ErrosValidacaoResult>.Fail(validacaoResult, command.Notifications));
            }

            return await Task.FromResult(Result.Ok());
        }
    }
}
