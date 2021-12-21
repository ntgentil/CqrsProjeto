using BaseCore.Commands;
using BaseCore.Validation.Notifications;
using BaseCore.Validation.Validations.Contracts;

namespace Core.Application.Importacao.Commands.Inputs
{
    public class ImportacaoCommand : Notifiable, IValidatable, ICommand
    {
        public string File { get; set; }
        
        public void Validate()
        {
            throw new System.NotImplementedException();
        }
    }
}
