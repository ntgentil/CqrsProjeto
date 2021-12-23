using System;
using System.Collections.Generic;

namespace Core.Application.Importacao.Commands.Models
{
   

    public class ImportacaoEntity
    {
        public int Id { get; set; }
        public DateTime DataCadastro { get; set; } = DateTime.Now;

        public ICollection<ImportacaoProdutoEntity> RelacoesImportacaoProduto { get; set; }
    }
}
