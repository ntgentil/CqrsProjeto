using System.Threading.Tasks;

namespace BaseCore.Queries
{
    public interface IPagedQueryHandler<in TPagedQueryParameters, TResult>
        where TPagedQueryParameters : IPagedQuery<PagingParameters>
    {
        Task<TResult> ExecuteQueryAsync(TPagedQueryParameters queryParameters);
    }
}
