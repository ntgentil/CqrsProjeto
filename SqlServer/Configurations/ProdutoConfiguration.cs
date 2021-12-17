﻿using Core.Application.Importacao.Commands.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SqlServer.Configurations
{
    
    public class ProdutoConfiguration : IEntityTypeConfiguration<ProdutoEntity>
    {
        public void Configure(EntityTypeBuilder<ProdutoEntity> builder)
        {
            builder.ToTable("PRODUTO");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .HasColumnName("ID");

            builder.Property(x => x.Nome)
                .HasColumnName("NOME");

            builder.Property(x => x.Quantidade)
               .HasColumnName("QUANTIDADE");

            builder.Property(x => x.DataEntrega)
               .HasColumnName("DATAENTREGA");

            builder.Property(x => x.Valor)
               .HasColumnName("VALOR");
        }
    }
}
