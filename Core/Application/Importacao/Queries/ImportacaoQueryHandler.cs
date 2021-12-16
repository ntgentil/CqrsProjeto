﻿using BaseCore.Queries;
using Core.Application.Importacao.Queries.Inputs;
using Core.Application.Importacao.Queries.Results;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.Application.Importacao.Queries
{
    public class ImportacaoQueryHandler :
        IQueryHandler<ProdutoIdInput, ProdutoResult>
    {
        private ImportacaoQuery ImportacaoQuery { get; }

        public ImportacaoQueryHandler(ImportacaoQuery importacaoQuery)
        {
            ImportacaoQuery = importacaoQuery;
        }

        public async Task<ProdutoResult> ExecuteAsync(ProdutoIdInput parameters)
        {
            return await ImportacaoQuery.GetProduto(parameters);
        }
    }
}