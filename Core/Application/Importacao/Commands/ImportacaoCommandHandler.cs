using BaseCore.Commands;
using BaseCore.Data;
using Core.Adapters.Queries;
using Core.Adapters.SqlServer;
using Core.Application.Importacao.Commands.Inputs;
using Core.Application.Importacao.Commands.Models;
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
                try
                {
                    var importacao = new ImportacaoEntity();
                    UnitOfWork.ImportacaoRepository.Insert(importacao);
                    

                    foreach (var item in command.Produtos)
                    {
                        var produto = new ProdutoEntity
                        {
                            DataEntrega = DateTime.Now,
                            Nome = item.Nome,
                            Quantidade = item.Quantidade,
                            Valor = item.Valor
                        };
                        UnitOfWork.ProdutoRepository.Insert(produto);

                        UnitOfWork.ImportacaoProdutoRepository.Insert(
                            new ImportacaoProdutoEntity
                            {
                                ImportacaoId = importacao.Id,
                                Importacao = importacao,
                                Produto = produto,
                                ProdutoId = produto.Id
                            }
                        );

                    }

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
