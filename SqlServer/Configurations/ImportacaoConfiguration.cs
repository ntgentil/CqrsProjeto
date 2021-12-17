using Core.Application.Importacao.Commands.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SqlServer.Configurations
{
    public class ImportacaoConfiguration : IEntityTypeConfiguration<ImportacaoEntity>
    {
        public void Configure(EntityTypeBuilder<ImportacaoEntity> builder)
        {
            builder.ToTable("IMPORTACAO");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .HasColumnName("ID");

            builder.Property(x => x.DataCadastro)
              .HasColumnName("DATACADASTRO");

        }
    }
}
