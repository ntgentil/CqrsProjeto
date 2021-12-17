namespace Core.Application.Importacao.Commands.Models
{
    public class ImportacaoProdutoEntity
    {
        public long ImportacaoId { get; set; }
        public long ProdutoId { get; set; }

        public ImportacaoEntity Importacao { get; set; }
        public ProdutoEntity Produto { get; set; }
    }
}
