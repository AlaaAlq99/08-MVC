using GymManagmentSystem_DAL.Models;

namespace GymManagmentSystem_DAL.Repositries.Classes
{
    public interface IRepository<TEntity> where TEntity : BaseEntity, new()
    {
        Task<int> AddAsync(TEntity entity);
        Task<int> DeleteAsync(TEntity entity);
        Task<IEnumerable<TEntity>> GetAllAsync(bool tracking = false);
        Task<TEntity?> GetByIdAsync(int id);
        Task<int> UpdateAsync(TEntity entity);
    }
}