using Core.Application.Importacao.Queries;
using Core.Application.Importacao.Queries.Inputs;
using Core.Application.Importacao.Queries.Results;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.Adapters.Queries
{
    public interface IImportacaoQuery
    {
        Task<List<ProdutoDto>> GetImportacao(ImportacaoIdInput produtoId);
        Task<ImportacoesResult> GetImportacoes();
    }
}
