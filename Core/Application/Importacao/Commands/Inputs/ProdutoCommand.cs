using System;

namespace Core.Application.Importacao.Commands.Inputs
{
    public class ProdutoCommand
    {
        public int Id { get; set; }
        public int Linha{ get; set; }
        public string Nome { get; set; }
        public long Quantidade { get; set; }
        public DateTime DataEntrega { get; set; }
        public decimal Valor { get; set; }
    }
}
