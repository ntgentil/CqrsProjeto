using Core.Adapters.SqlServer;
using Core.Application.Importacao.Commands.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlServer
{
    public class DomainUnitOfWork : IUnitOfWork
    {
        private readonly DomainDataContext _db;

        private Repository<ProdutoEntity> produtoRepository;
        private Repository<ImportacaoEntity> importacaoRepository;
        private Repository<ImportacaoProdutoEntity> importacaoProdutoRepository;
        public Repository<ProdutoEntity> ProdutoRepository => produtoRepository ??= new Repository<ProdutoEntity>(_db);
        public Repository<ImportacaoEntity> ImportacaoRepository => importacaoRepository ??= new Repository<ImportacaoEntity>(_db);
        public Repository<ImportacaoProdutoEntity> ImportacaoProdutoRepository => importacaoProdutoRepository ??= new Repository<ImportacaoProdutoEntity>(_db);

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="dbContext"></param>
        public DomainUnitOfWork(DomainDataContext dbContext) => _db = dbContext;

        /// <summary>
        /// Commit All Changes
        /// </summary>
        public void Commit() => _db.SaveChanges();

        /// <summary>
        /// Rollback
        /// </summary>
        public void Rollback() => _db.Dispose();

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _db.Dispose();
                }
            }
            this.disposed = true;
        }
    }
}
