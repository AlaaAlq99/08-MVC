using GymManagmentSystem_DAL.Data.Context;
using GymManagmentSystem_DAL.Models;
using GymManagmentSystem_DAL.Repositries.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GymManagmentSystem_DAL.Repositries.Classes
{
    public class Repository<TEntity> : IRepository<TEntity, int> where TEntity : BaseEntity, new()
    {
        protected readonly GymDbContext _dbContext;
        protected readonly DbSet<TEntity> _set;

        public Repository(GymDbContext dbContext)
        {
            _dbContext = dbContext;
            _set = _dbContext.Set<TEntity>();
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await _set.AsNoTracking().ToListAsync();
        }

        public async Task<TEntity?> GetByIdAsync(int id)
        {
            return await _set.FindAsync(id);
        }

        public async Task AddAsync(TEntity entity)
        {
            await _set.AddAsync(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(TEntity entity)
        {
            _set.Update(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await GetByIdAsync(id);
            if (entity != null)
            {
                _set.Remove(entity);
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}