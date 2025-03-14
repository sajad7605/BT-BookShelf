using BookStoreApp.Interfaces;

namespace BookStoreApp.Repositories{
    
        public class GenericRepo<T> : IRepository<T> where T : class
        {
            private readonly AppDbContext _Context;
            private readonly DbSet<T> _Table;
            public GenericRepo(AppDbContext context)
            {
                _Context=context;
                _Table=context.Set<T>();

            }
            public Task<bool> AddAsync(T entity)
            {
                throw new NotImplementedException();
            }

            public Task<bool> DeleteAsync(T entity)
            {
                throw new NotImplementedException();
            }

            public Task<bool> DeleteByIdAsync(int id)
            {
                throw new NotImplementedException();
            }

            public async Task<IEnumerable<T>> GetAllAsync()
            {
                return await _Table.ToListAsync();
            }

            public Task<T?> GetByIdAsync(int id)
            {
                throw new NotImplementedException();
            }

            public bool Save()
            {
                throw new NotImplementedException();
            }

            public Task<bool> UpdateAsync(T entity)
            {
                throw new NotImplementedException();
            }
        
    }
}