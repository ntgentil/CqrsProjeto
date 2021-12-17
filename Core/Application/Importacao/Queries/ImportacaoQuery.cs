using Core.Adapters.Queries;
using Core.Adapters.SqlServer;
using Core.Application.Importacao.Queries.Inputs;
using Core.Application.Importacao.Queries.Results;
using System;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Core.Application.Importacao.Queries
{
    public class ImportacaoQuery : IImportacaoQuery
    {

        private readonly ISqlServerStoreHolder _sqlServerStoreHolder;

        public ImportacaoQuery(ISqlServerStoreHolder sqlServerStoreHolder)
        {
            _sqlServerStoreHolder = sqlServerStoreHolder;
        }

        public async Task<ProdutoResult> GetProduto(ProdutoIdInput param)
        {
            var result = new ProdutoResult();
            await using var conn = (SqlConnection)_sqlServerStoreHolder.GetDbConnection("CQRS");

            const string query = @"SELECT * FROM PRODUTO WHERE ID= @id";

            using (SqlCommand command = new SqlCommand(query, conn))
            {
                command.Parameters.AddWithValue("@id", param.Id);
                conn.Open();
                using (SqlDataReader dataReader = command.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        result.Id = Convert.ToInt32(dataReader["ID"]);
                        result.Nome = dataReader["NOME"].ToString();
                    }
                }
            }

            

            return result;
        }

        public async Task<ProdutosResult> GetProdutos()
        {
            var result = new ProdutosResult();
            await using var conn = (SqlConnection)_sqlServerStoreHolder.GetDbConnection("CQRS");

            const string query = @"SELECT * FROM PRODUTO";

            using (SqlCommand command = new SqlCommand(query, conn))
            {
                conn.Open();
                using (SqlDataReader dataReader = command.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        result.Produtos.Add(new ProdutoResult { Id = Convert.ToInt32(dataReader["ID"]), Nome = dataReader["NOME"].ToString()});
                    }
                }
            }

            return result;
        }
    }
}
