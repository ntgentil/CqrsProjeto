using Core.Application.Importacao.Commands.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Adapters.SqlServer
{
    public interface IUnitOfWork : IDisposable
    {
        public Repository<ProdutoEntity> ProdutoRepository { get; }
        //public Repository<HistoricoMensagemEntity> HistoricoMensagemRepository { get; }
        void Commit();
        void Rollback();
    }
}
