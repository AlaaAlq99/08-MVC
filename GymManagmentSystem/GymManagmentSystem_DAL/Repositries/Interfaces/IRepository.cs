using GymManagmentSystem_DAL.Models;
using System.Linq.Expressions;

namespace GymManagmentSystem_DAL.Repositries.Interfaces
{
    public interface IRepository<TEntity> where TEntity : BaseEntity, new()
    {
       public IQueryable<TEntity> GetAll(bool traking = false);
       public Task<IEnumerable<TEntity>> GetAllAsync();
       public Task<TEntity> GetByIdAsync(int id);
       public Task AddAsync(TEntity entity);
       public Task UpdateAsync(TEntity entity);
       public Task DeleteAsync(int id);


        public Task<int> CountAsync(Expression<Func<TEntity,bool>?>  condition = null);



    }
}
