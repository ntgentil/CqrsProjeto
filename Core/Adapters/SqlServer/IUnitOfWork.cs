using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Adapters.SqlServer
{
    public interface IUnitOfWork : IDisposable
    {
        public Repository<MensagemCriticaEntity> MensagemCriticaRepository { get; }
        public Repository<HistoricoMensagemEntity> HistoricoMensagemRepository { get; }
        void Commit();
        void Rollback();
    }
}
