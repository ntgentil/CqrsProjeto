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
                   .IsNotNullOrEmpty(item.Nome, "Nome", $"Linha:{item.Linha} - Campo obrigatório")
                   .HasMaxLen(item.Nome, 50 , "Nome", $"Linha:{item.Linha} - O campo descrição precisa ter o tamanho máximo de 50 caracteres")
                   .IsGreater(item.Quantidade, 1, "Quantidade", $"Linha:{item.Linha} - Campo deve ser maior que 0")
                   .IsGreaterThan(item.DataEntrega,System.DateTime.Now, "DataEntrega", $"Linha:{item.Linha} - O campo data de entrega não pode ser menor ou igual que o dia atual")
               );
            }
           
        }
    }
}
