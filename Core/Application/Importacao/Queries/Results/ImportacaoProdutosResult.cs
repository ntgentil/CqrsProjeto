using System;
using System.Collections.Generic;
using System.Linq;

namespace Core.Application.Importacao.Queries.Results
{
    public class ImportacaoProsdutosResult
    {
        public List<ProdutoDto> Produtos { get; set; } = new List<ProdutoDto>();
        public decimal ValorTotal { get; set; }

        internal static ImportacaoProsdutosResult Parse(List<ProdutoDto> produtos)
        {
            return new ImportacaoProsdutosResult
            {
                Produtos = produtos,
                ValorTotal = produtos.Sum(p => p.Valor)
            };
        }

    }
}
