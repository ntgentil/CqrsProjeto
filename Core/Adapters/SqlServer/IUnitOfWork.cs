using Core.Application.Importacao.Commands.Models;
using System;

namespace Core.Adapters.SqlServer
{
    public interface IUnitOfWork : IDisposable
    {
        public Repository<ProdutoEntity> ProdutoRepository { get; }
        public Repository<ImportacaoEntity> ImportacaoRepository { get; }
        public Repository<ImportacaoProdutoEntity> ImportacaoProdutoRepository { get; }
        void Commit();
        void Rollback();
    }
}
