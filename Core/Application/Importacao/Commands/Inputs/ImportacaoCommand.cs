using BaseCore.Commands;
using BaseCore.Validation.Notifications;
using BaseCore.Validation.Validations;
using BaseCore.Validation.Validations.Contracts;
using System.Collections.Generic;

namespace Core.Application.Importacao.Commands.Inputs
{
    public class ImportacaoCommand : Notifiable, IValidatable, ICommand
    {
        public IList<ProdutoCommand> Produtos { get; set; } = new List<ProdutoCommand>();
        
        public void Validate()
        {
            foreach (var item in Produtos)
            {
                AddNotifications(
                 new Contract().Requires()
                   .IsNotNullOrEmpty(item.Nome, "Nome", "Não pode ser nulo")
                   .HasMaxLen(item.Nome, 2, "Nome", $"Linha:{item.Linha} - Tamanho maximo excedido")
               );
            }
           
        }
    }
}
