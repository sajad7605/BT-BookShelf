namespace BookStoreApp.Interfaces{
    public interface IRepository<T> where T: class
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T?> GetByIdAsync(int id);
        Task<bool> AddAsync(T entity);
        Task<bool> UpdateAsync(T entity);
        Task<bool> DeleteAsync(T entity);
        Task<bool> DeleteByIdAsync(int id);
        Task<bool> AddRangeAsync(IEnumerable<T> entities);
        Task<IEnumerable<T>> GetRangeByIdsAsync(IEnumerable<int> Ids);
        Task<bool> Save();

    }
}