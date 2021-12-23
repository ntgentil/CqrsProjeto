using Core.Application.Importacao.Commands.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlServer.Configurations
{
    public class ImportacaoProdutoConfiguration : IEntityTypeConfiguration<ImportacaoProdutoEntity>
    {
        public void Configure(EntityTypeBuilder<ImportacaoProdutoEntity> builder)
        {
            builder.ToTable("IMPORTACAO_PRODUTO");

            builder.HasKey(x => new { x.ImportacaoId, x.ProdutoId });

            builder.Property(x => x.ImportacaoId)
                .HasColumnName("IMPORTACAO_ID");

            builder.Property(x => x.ProdutoId)
                .HasColumnName("PRODUTO_ID");

            builder.HasOne(htmg => htmg.Importacao)
                 .WithMany(htcl => htcl.RelacoesImportacaoProduto)
                 .HasForeignKey(htcl => htcl.ImportacaoId);

            builder.HasOne(htmg => htmg.Produto)
                .WithMany(mgct => mgct.RelacoesImportacaoProduto)
                .HasForeignKey(htcl => htcl.ProdutoId);

        }
    }
}
