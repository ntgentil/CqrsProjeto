using System;
using System.Collections.Generic;

namespace Core.Application.Importacao.Commands.Models
{
    public class ProdutoEntity
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public int Quantidade { get; set; }
        public DateTime DataEntrega { get; set; }
        public decimal Valor { get; set; }

        public ICollection<ImportacaoProdutoEntity> RelacoesImportacaoProduto { get; set; }

    }
}
