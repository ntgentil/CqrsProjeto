using Core.Adapters.Queries;
using Core.Adapters.SqlServer;
using Core.Application.Importacao.Queries.Inputs;
using Core.Application.Importacao.Queries.Results;
using System;
using System.Collections.Generic;
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

        public async Task<List<ProdutoDto>> GetImportacao(ImportacaoIdInput param)
        {
            var result = new List<ProdutoDto>();
            await using var conn = (SqlConnection)_sqlServerStoreHolder.GetDbConnection("CQRS");

            const string query = @"SELECT PROD.ID, PROD.NOME, PROD.QUANTIDADE, PROD.VALOR, PROD.DATAENTREGA  
                                FROM PRODUTO AS PROD
                                join IMPORTACAO_PRODUTO as IM_P on IM_P.PRODUTO_ID = PROD.ID
                                join IMPORTACAO as IMP on IMP.ID = IM_P.IMPORTACAO_ID
                                WHERE IMP.ID= @id";

            using (SqlCommand command = new SqlCommand(query, conn))
            {
                command.Parameters.AddWithValue("@id", param.Id);
                conn.Open();
                using (SqlDataReader dataReader = command.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        result.Add(new ProdutoDto
                        {
                            Id = Convert.ToInt32(dataReader["ID"]),
                            Nome = dataReader["NOME"].ToString(),
                            Quantidade = Convert.ToInt32(dataReader["QUANTIDADE"]),
                            DataEntrega = Convert.ToDateTime(dataReader["DATAENTREGA"]),
                            Valor = Convert.ToDecimal(dataReader["VALOR"]),
                        });
                    }
                }
            }

            return result;
        }

        public async Task<ImportacoesResult> GetImportacoes()
        {
            var result = new ImportacoesResult();
            await using var conn = (SqlConnection)_sqlServerStoreHolder.GetDbConnection("CQRS");

            const string query = @"SELECT 
	                                IMP.ID, 
	                                IMP.DATACADASTRO as DATA_IMPORTACAO,
	                                MIN(PROD.DATAENTREGA) as MIN_DATA_ENTREGA, 
	                                SUM(PROD.QUANTIDADE) as QUANTIDADE, 
	                                SUM(PROD.VALOR) as VALOR_TAOTAL 
                                FROM IMPORTACAO as IMP
                                JOIN IMPORTACAO_PRODUTO IPRO on IPRO.IMPORTACAO_ID = IMP.ID
                                JOIN PRODUTO as PROD on PROD.ID = IPRO.PRODUTO_ID
                                GROUP BY IMP.ID, IMP.DATACADASTRO";

            using (SqlCommand command = new SqlCommand(query, conn))
            {
                conn.Open();
                using (SqlDataReader dataReader = command.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        result.Importacoes.Add(
                            new ImportacaoResult(
                                Convert.ToInt32(dataReader["ID"]),
                                Convert.ToDateTime(dataReader["DATA_IMPORTACAO"]),
                                Convert.ToInt32(dataReader["QUANTIDADE"]),
                                Convert.ToDateTime(dataReader["MIN_DATA_ENTREGA"]),
                                Convert.ToDecimal(dataReader["VALOR_TAOTAL"])
                            )
                         );
                    }
                }

                return result;
            }
        }
    }
}
