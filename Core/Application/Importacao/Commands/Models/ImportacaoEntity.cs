using System;
using System.Collections.Generic;

namespace Core.Application.Importacao.Commands.Models
{
   

    public class ImportacaoEntity
    {
        public long Id { get; set; }
        public DateTime DataEntrega { get; set; }
        public DateTime DataCadastro { get; set; } = DateTime.Now;

        public ICollection<ImportacaoProdutoEntity> RelacoesImportacaoProduto { get; set; }
    }
}
