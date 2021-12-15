using System.Data.SqlClient;

namespace Core.Adapters.SqlServer
{
    /// <summary>
    /// Classe para abstrari uma chamada atômica ADO SQL
    /// </summary>
    /// <typeparam name="TParam"></typeparam>
    /// <typeparam name="TResult"></typeparam>
    public abstract class SqlQuery<TParam, TResult>
    {
        /// <summary>
        /// Método no qual deve ser escrito o Script SQL.
        /// Evite atriburi parâmetros diretamente na Query. Em vez disso,
        /// utilize o método BeforeExecute(TParam) para atribuir parâmetros ao SqlCommand
        /// </summary>
        /// <returns></returns>
        protected abstract string PrepareQuery();

        /// <summary>
        /// Executado antes do método "Execute()"
        /// utilize o método BeforeExecute(TParam) para atribuir parâmetros ao SqlCommand
        /// da classe exposto na propriedade Command
        /// </summary>
        /// <param name="param"></param>
        protected virtual void BeforeExecute(SqlCommand command, TParam param)
        {
            return;
        }

        /// <summary>
        /// Executado após o método "Execute()"
        /// utilize para transformar o DataReader
        /// </summary>
        /// <param name="param"></param>
        protected virtual TResult AfterExecute(SqlDataReader reader)
        {
            return default;
        }

        /// <summary>
        /// Executado após o método "Execute()"
        /// utilize para transformar o próprio Resultado
        /// </summary>
        /// <param name="param"></param>
        protected virtual TResult AfterExecute(TResult result)
        {
            return result;
        }

        /// <summary>
        /// utilize para customizar a execução da Query
        /// </summary>
        /// <param name="param"></param>
        protected virtual TResult OnExecuting()
        {
            return default;
        }

        /// <summary>
        /// Método principal de Execução.
        /// Em seu escopo essa ação tentará chamar o OnExecuting(). Caso esse método não 
        /// seja implementado ele chamará um método ExecuteReader() que retornará um DataReader
        /// que deve ser tratado através da customização do método AfterExecute(DataReader reader).
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public TResult Execute(SqlConnection connection, TParam param)
        {
            var command = new SqlCommand(PrepareQuery(), connection);

            BeforeExecute(command, param);
            return AfterExecute(OnExecuting()) ?? ExecuteReader(connection, command);
        }

        private TResult ExecuteReader(SqlConnection connection, SqlCommand command)
        {
            TResult result = default;

            using (command)
            {
                connection.Open();

                using (SqlDataReader dataReader = command.ExecuteReader())
                {
                    result = AfterExecute(dataReader);
                }

                connection.Close();
            }

            return result;
        }

    }
}
