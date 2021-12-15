namespace BaseCore.Queries
{
    public interface IPagedQuery<in TPagedQueryParameters>
        where TPagedQueryParameters : PagingParameters
    {
    }
}
