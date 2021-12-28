using System;

namespace Core.Application.Importacao.Queries.Results
{
    public class ImportacaoResult
    {
        public int Id { get; set; }
        public DateTime DataImportacao { get; set; }
        public int Quantidade { get; set; }
        public DateTime DataEntrega { get; set; }
        public decimal ValorTotal { get; set; }

        public ImportacaoResult(int id, DateTime dataImportacao, int quantidade, DateTime dataEntrega, decimal valorTotal)
        {
            Id = id;
            DataImportacao = dataImportacao;
            Quantidade = quantidade;
            DataEntrega = dataEntrega;
            ValorTotal = valorTotal;
        }

    }
}
