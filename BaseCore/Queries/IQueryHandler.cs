using System.Threading.Tasks;

namespace BaseCore.Queries
{
    public interface IQueryHandler<in TParameters, TResult>
         where TParameters : IQuery
    {
        Task<TResult> ExecuteAsync(TParameters parameters);
    }
}
