using BaseCore.Enumerations;
using BaseCore.Validation.Notifications;
using Core.Application.Importacao.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Importacao.Commands.Results
{
    public class ErrosValidacaoResult
    {
        public IList<ValidacaoImportacao> Erros { get; set; }

        public static ErrosValidacaoResult Parse(IReadOnlyCollection<Notification> notifications)
        {
            var enumeradoresValidacaoCalculo = Enumeration.GetAll<ValidacaoImportacao>();
            var erros = notifications.Select
            (
                n => enumeradoresValidacaoCalculo.FirstOrDefault(en => en.Id == n.Property && en.Name == n.Message)
            ).ToList();

            return new ErrosValidacaoResult { Erros = erros };
        }
    }
}
