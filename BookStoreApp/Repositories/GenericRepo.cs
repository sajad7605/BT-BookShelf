using System.Threading.Tasks;
using BookStoreApp.Interfaces;

namespace BookStoreApp.Repositories
{

    public class GenericRepo<T> : IRepository<T> where T : class
    {
        private readonly AppDbContext _Context;
        private readonly DbSet<T> _Table;
        public GenericRepo(AppDbContext context)
        {
            _Context = context;
            _Table = context.Set<T>();

        }
        public async Task<bool> AddAsync(T entity)
        {
            _Context.Add<T>(entity);
            return await Save();
        }

        public async Task<bool> AddRangeAsync(IEnumerable<T> entities)
        {
            await _Table.AddRangeAsync(entities);
            return await Save();
        }

        public async Task<bool> DeleteAsync(T entity)
        {
            _Context.Remove<T>(entity);
            return await Save();
        }

        public async Task<bool> DeleteByIdAsync(int id)
        {
            T? obj = await GetByIdAsync(id);
            if (obj is null)
            {
                throw new NullReferenceException("the object was null! can't remove a null obj");
            }

            return await DeleteAsync(obj);
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _Table.ToListAsync();
        }

        public async Task<T?> GetByIdAsync(int id)
        {
            return await _Table.AsNoTracking().FirstOrDefaultAsync(e => EF.Property<int>(e, "Id") == id);
        }

        public async Task<T?> GetByNameAsync(string name)
        {
            return await _Table.FirstOrDefaultAsync(Name=> EF.Property<string>(Name,"Name") == name);
        }

        public async Task<IEnumerable<T>> GetRangeByIdsAsync(IEnumerable<int> Ids)
        {
            IEnumerable<T> entities=await _Table.Where(e=> Ids.Contains(EF.Property<int>(e,"Id"))).ToListAsync();
            return entities;

        }

        public async Task<bool> Save()
        {
            var result = await _Context.SaveChangesAsync();
            return result > 0 ? true : false;
        }

        public async Task<bool> UpdateAsync(T entity)
        {
            _Context.Update<T>(entity);
            return await Save();
        }

    }
}