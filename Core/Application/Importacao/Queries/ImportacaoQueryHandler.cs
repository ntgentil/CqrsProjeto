using BaseCore.Queries;
using Core.Application.Importacao.Queries.Inputs;
using Core.Application.Importacao.Queries.Results;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.Application.Importacao.Queries
{
    public class ImportacaoQueryHandler :
        IQueryHandler<ImportacaoIdInput, ImportacaoProsdutosResult>,
        IQueryHandler<ImportacaoAllInput, ImportacoesResult>
    {
        private ImportacaoQuery ImportacaoQuery { get; }

        public ImportacaoQueryHandler(ImportacaoQuery importacaoQuery)
        {
            ImportacaoQuery = importacaoQuery;
        }

        public async Task<ImportacaoProsdutosResult> ExecuteAsync(ImportacaoIdInput parameters)
        {
            var produtos = await ImportacaoQuery.GetImportacao(parameters);
            return ImportacaoProsdutosResult.Parse(produtos);
        }

        public async Task<ImportacoesResult> ExecuteAsync(ImportacaoAllInput parameters)
        {
            return await ImportacaoQuery.GetImportacoes();
        }
    }
}
