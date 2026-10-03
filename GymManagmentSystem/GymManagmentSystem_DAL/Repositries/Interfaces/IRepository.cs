using GymManagmentSystem_DAL.Models;

namespace GymManagmentSystem_DAL.Repositries.Interfaces
{
    public interface IRepository<TEntity, TKey> where TEntity : BaseEntity, new()
    {
       public Task<IEnumerable<TEntity>> GetAllAsync();
       public Task<TEntity> GetByIdAsync(TKey id);
       public Task<int> AddAsync(TEntity entity);
       public Task UpdateAsync(TEntity entity);
       public Task DeleteAsync(TKey id);
    }
}
