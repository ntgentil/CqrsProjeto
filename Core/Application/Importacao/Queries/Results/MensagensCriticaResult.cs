using System.Collections.Generic;

namespace Core.Application.Importacao.Queries.Results
{
    public class MensagensCriticaResult
    {
        public IList<MensagemCriticaResult> Mensagens { get; set; }

        public MensagensCriticaResult()
        {
            Mensagens = new List<MensagemCriticaResult>();
        }
    }
}
