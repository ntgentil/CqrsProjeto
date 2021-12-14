using Core.Application.Importacao.Enumerations;
using Core.Application.Importacao.Queries.Results;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.Adapters.Queries
{
    public interface IImportacaoQuery
    {
        Task<MensagensCriticaResult> ListarMensagensPorEnumeradores(IList<ValidacaoImportacao> validacoesCalculos);
    }
}
