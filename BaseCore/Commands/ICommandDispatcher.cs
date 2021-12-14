using BaseCore.Data;
using System.Threading.Tasks;

namespace BaseCore.Commands
{
    public interface ICommandDispatcher
    {
        Task<IResult> ExecuteAsync<TCommand>(TCommand command)
            where TCommand : ICommand;
    }
}
