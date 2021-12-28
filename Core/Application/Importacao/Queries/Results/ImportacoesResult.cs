using System;
using System.Collections.Generic;

namespace Core.Application.Importacao.Queries.Results
{
    public class ImportacoesResult
    {
        public List<ImportacaoResult> Importacoes { get; set; }

        public ImportacoesResult()
        {
            Importacoes = new List<ImportacaoResult>();
        }
    }

    
}
