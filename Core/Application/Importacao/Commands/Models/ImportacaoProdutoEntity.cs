namespace Core.Application.Importacao.Commands.Models
{
    public class ImportacaoProdutoEntity
    {
        public int ImportacaoId { get; set; }
        public int ProdutoId { get; set; }

        public ImportacaoEntity Importacao { get; set; }
        public ProdutoEntity Produto { get; set; }
    }
}
