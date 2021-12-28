using System;

namespace Core.Application.Importacao.Queries
{
    public class ProdutoDto
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public long Quantidade { get; set; }
        public DateTime DataEntrega { get; set; }
        public decimal Valor { get; set; }
    }
}
