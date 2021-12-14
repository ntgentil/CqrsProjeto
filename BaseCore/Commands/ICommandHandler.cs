using BaseCore.Data;
using System.Threading.Tasks;

namespace BaseCore.Commands
{
    public interface ICommandHandler<in TCommand>
         where TCommand : ICommand
    {
        Task<IResult> ExecuteAsync(TCommand command);
    }
}
