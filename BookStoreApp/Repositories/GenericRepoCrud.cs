using BookStoreApp.Interfaces;

namespace BookStoreApp.Repositories{
    
        public class GenericRepo<T> : IRepository<T> where T : class
        {
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

            public Task<IEnumerable<T>> GetAllAsync()
            {
                throw new NotImplementedException();
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