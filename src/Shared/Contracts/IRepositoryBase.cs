namespace Shared.Contracts
{
    public interface IRepositoryBase<T> where T : class
    {
        Task InsertAsync(T model);
    }
}