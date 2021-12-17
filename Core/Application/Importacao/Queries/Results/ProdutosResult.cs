using System.Collections.Generic;

namespace Core.Application.Importacao.Queries.Results
{
    public class ProdutosResult
    {
        public List<ProdutoResult> Produtos { get; set; }

        public ProdutosResult()
        {
            Produtos = new List<ProdutoResult>();
        }
    }
}
