using GymManagmentSystem_DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GymManagmentSystem_DAL.Repositries.Interfaces;
using GymManagmentSystem_DAL.Data.Context;

namespace GymManagmentSystem_DAL.Repositries.Classes
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly GymDbContext _dbContext;

        public UnitOfWork(GymDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        private readonly Dictionary<string, object> Repos = [];
        public IRepository<TEntity> GetRepository<TEntity>() where TEntity : BaseEntity, new()
        {

            var type = typeof(TEntity).Name;
            if(!string.IsNullOrEmpty(type) && Repos.TryGetValue(type, out object repository))
            {
                return (IRepository<TEntity>)Repos[type];
            }
           
            var repo = new Repository<TEntity>(_dbContext);
            Repos[type] = repo;
            return repo;

        }

       public  async Task<int> SaveChangesAsync()
        {
            return await _dbContext.SaveChangesAsync();
        }

       
    }
}
