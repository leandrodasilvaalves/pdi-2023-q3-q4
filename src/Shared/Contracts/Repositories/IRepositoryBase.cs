namespace Shared.Contracts.Repositories
{
    public interface IRepositoryBase<T> where T : class
    {
        Task InsertAsync(T model);
    }
}